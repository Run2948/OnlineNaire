using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuickWeb.Models.RequestModel;

namespace QuickWeb.Controllers
{
    public class QuestionnaireController : Controller
    {
        // GET: Questionnaire

        #region 后台主页面
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        #endregion

        #region 问卷列表
        public ActionResult List()
        {
            return View();
        }

        public ActionResult PagedList(BaseRequest request)
        {
            return PartialView();
        } 
        #endregion

        #region 创建问卷

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateSave()
        {
            return View();
        }

        #endregion

        #region 问卷复制-删除

        [HttpGet]
        public ActionResult QuestionnaireCopy(long ?id)
        {
            return Content("ok");
        }

        [HttpGet]
        public ActionResult QuestionnaireDelete(long ?id)
        {
            return Content("ok");
        }

        #endregion

        #region 问卷暂停-开始/发布

        [HttpGet]
        public ActionResult QuestionnaireStop(long ?id)
        {
            return Content("ok");
        }

        [HttpGet]
        public ActionResult QuestionnaireStart(long ?id)
        {
            return Content("ok");
        }

        #endregion

        #region 问卷管理

        [HttpGet]
        public ActionResult QuestionnaireManager(long questionnaireId, string type)
        {
            ViewBag.Type = type;
            return View();
        } 

        #region 1.问卷名称
        [HttpGet]
        public ActionResult QuestionnaireEdit(long questionnaireId)
        {
            return View();
        } 
        #endregion

        #region 2.问卷题目
        [HttpGet]
        public ActionResult QuestionManager(long questionnaireId)
        {
            return View();
        } 
        #endregion

        #region 3.问卷设置
        [HttpGet]
        public ActionResult SettingManager(long questionnaireId)
        {
            return View();
        } 
        #endregion
       
        #region 4.问卷传播
        [HttpGet]
        public ActionResult SpreadManager(long questionnaireId)
        {
            return View();
        }
        #endregion

        #region 5.问卷回收
        [HttpGet]
        public ActionResult RecyclingManager(long questionnaireId)
        {
            return View();
        }

        public ActionResult RecyclingPagedList(RecyclingRequest request)
        {
            return PartialView();
        }
        #endregion

        #region 6.单题分析
        [HttpGet]
        public ActionResult SingleAnalysisManager(long questionnaireId)
        {
            return View();
        }
        #endregion

        #region 7.交叉分析
        [HttpGet]
        public ActionResult CrossAnalysisManager(long questionnaireId)
        {
            return View();
        }
        #endregion

        #endregion
    }
}