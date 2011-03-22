using System.Web.Mvc;
using _2SQUARE.Models;

namespace _2SQUARE.Controllers
{
    /// <summary>
    /// All controllers to be used for risk assessment types will need to inherit from this interface
    /// Other controllers automatically refer to this action
    /// </summary>
    public interface IProcedureController
    {
        ActionResult Index(int id /* project step id */, int projectId);
    }
}