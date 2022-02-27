using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using cemetery.Models;
using cemetery.Data;
using PagedList.Core;
using PagedList.Core.Mvc;

namespace cemetery.Controllers;

public class DeedController : Controller
{
    private readonly ILogger<DeedController> _logger;    
    private readonly IDataRepository _dataRepo;

    public DeedController(ILogger<DeedController> logger, IDataRepository dataRepo)
    {
        _logger = logger;
        _dataRepo = dataRepo;
    }

    public async Task<IActionResult> Index(int? page)
    {
        var viewModel = new DeedViewModel();
        var deeds = await _dataRepo.GetDeeds();
        var pageNumber = page == null || page <= 0 ? 1 : page.Value;
        var pageSize = 25;
        StaticPagedList<Deed> deedspaged = new StaticPagedList<Deed>(deeds.OrderBy(x => x.IssueDate).Skip((pageNumber - 1) * pageSize).Take(pageSize), pageNumber, pageSize, deeds.Count());
        viewModel.Deeds = deedspaged;
        //get list data
        var cemeteries = await _dataRepo.GetCemeteries();
        var sections = await _dataRepo.GetSections();
        viewModel.Cemeteries = cemeteries.ToList() ?? new List<Cemetery>();
        viewModel.Sections = sections.ToList() ?? new List<Section>();
        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult EditDeed(Deed deed)
    {
        try {
            if (deed.id > 0)
            {
                _dataRepo.UpdateDeed(deed);
            } else {
                _dataRepo.AddDeed(deed);
            }
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "DeedController.EditDeed: {0}", exception.Message);
            return Json( new { status = "error", message = exception.Message });
        }
        return Json( new { status = "success", message = "Success" });
    }

    [HttpDelete]
    [Route("/Deed/{deedid}")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteDeed(int deedid)
    {
        try {
            _dataRepo.DeleteDeed(deedid);
        }
        catch(Exception exception)
        {
            _logger.LogError(exception, "DeedController.DeleteDeed: {0}", exception.Message);
            return Json(new { status = "error", message="Failed to delete deed"});
        }
        return Json(new {status="success", message="Success"});
    }
}
