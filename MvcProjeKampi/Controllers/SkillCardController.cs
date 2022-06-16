using BusinessLayer.Concreate;
using DataAccessLayer.EntitiyFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcProjeKampi.Controllers
{
    public class SkillCardController : Controller
    {
        // GET: SkillCard

        SkillCardManager scm = new SkillCardManager(new EfSkillCardDal());

        public ActionResult Index()
        {
            var values = scm.GetList();
            return View(values);
        }
    }
}