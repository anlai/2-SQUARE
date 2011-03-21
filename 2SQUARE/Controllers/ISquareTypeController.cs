using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace _2SQUARE.Controllers
{
    interface ISquareTypeController
    {
        /*
         * Id for all actions refer to project step id
         */

        ActionResult Step1(int id, int projectId);
        ActionResult Step2(int id, int projectId);
        ActionResult Step3(int id, int projectId);
        ActionResult Step4(int id, int projectId);
        ActionResult Step5(int id, int projectId);
        ActionResult Step6(int id, int projectId);
        ActionResult Step7(int id, int projectId);
        ActionResult Step8(int id, int projectId);
        ActionResult Step9(int id, int projectId);
    }
}
