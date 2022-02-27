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
}
