using Microsoft.AspNetCore.Mvc;
using cemetery.Models;
using cemetery.Data;
using PagedList.Core;
using PagedList.Core.Mvc;

namespace cemetery.Controllers;

public class CemeteryController : Controller
{
    private readonly ILogger<CemeteryController> _logger;
    private readonly IDataRepository _dataRepo;

    public CemeteryController(ILogger<CemeteryController> logger, IDataRepository dataRepo)
    {
        _logger = logger;
        _dataRepo = dataRepo;
    }

    public async Task<IActionResult> Index(int? page)
    {
        var cemeteries = await _dataRepo.GetCemeteries();
        var pageNumber = page == null || page <= 0 ? 1 : page.Value;
        var pageSize = 25;
        StaticPagedList<Cemetery> cemeteriesPaged = new StaticPagedList<Cemetery>(cemeteries.OrderBy(x => x.id).Skip((pageNumber - 1) * pageSize).Take(pageSize), pageNumber, pageSize, cemeteries.Count());
        return View(cemeteriesPaged);
    }

    public async Task<IActionResult> EditCemetery(int cemeteryid)
    {
        var cemetery = await _dataRepo.GetCemetery(cemeteryid);
        return View(cemetery);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult EditCemetery(Cemetery cemetery)
    {
        try {
            if (cemetery.id > 0)
            {
                _dataRepo.UpdateCemetery(cemetery);
            } else {
                _dataRepo.AddCemetery(cemetery);
            }
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "CemeteryController.EditCemetery: {0}", exception.Message);
            return Json(new { status="error",message = exception.Message });
        }
        return Json(new { status="success",message = "Success" });
    }

    [HttpDelete]
    [Route("/Cemetery/{cemeteryid}")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteCemetery(int cemeteryid)
    {
        try {
            _dataRepo.DeleteCemetery(cemeteryid);
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "CemeteryController.DeleteCemetery: {0}", exception.Message);
            return Json(new { status="error", message = "Failed to delete cemetery."});
        }
        return Json(new { status="success",message="Success"});
    }

    [Route("/Cemetery/{cemeteryid}/Sections")]
    public async Task<IActionResult> Sections(int? page, int cemeteryid)
    {
        var pageNumber = page == null || page <= 0 ? 1 : page.Value;
        var pageSize = 25;
        var sections = await _dataRepo.GetSectionsForCemetery(cemeteryid);
        var cemetery = await _dataRepo.GetCemetery(cemeteryid);
        StaticPagedList<Section> sectionsPaged = new StaticPagedList<Section>(sections.OrderBy(x => x.id).Skip((pageNumber - 1) * pageSize).Take(pageSize), pageNumber, pageSize, sections.Count());
        SectionsViewModel viewModel = new SectionsViewModel()
        {
            Cemetery = cemetery,
            Sections = sectionsPaged
        };
        return View(viewModel);
    }
}