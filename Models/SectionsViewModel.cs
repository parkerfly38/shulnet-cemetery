using PagedList.Core;

namespace cemetery.Models;

public class SectionsViewModel
{
    public Cemetery Cemetery { get; set; }

    public StaticPagedList<Section> Sections { get; set; }
}