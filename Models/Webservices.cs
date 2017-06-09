using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Entities.Users;
using DotNetNuke.Security;
using DotNetNuke.Web.Api;

namespace Christoc.Modules.MyFirstModule.Models
{
    public class ModuleTaskController : DnnApiController
    {
        [AllowAnonymous]
        [HttpGet]
        public HttpResponseMessage HelloWorld()
        {
            return Request.CreateResponse(HttpStatusCode.OK, "Hello World");
        }


        [AllowAnonymous]
        [HttpGet]
        public HttpResponseMessage GetTasks(int moduleId)
        {
            try
            {
                var tasks = new TaskController().GetTasks(moduleId).ToJson();
                return Request.CreateResponse(HttpStatusCode.OK, tasks);
            }

            catch (Exception exc)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, exc);
            }

        }


        [AllowAnonymous]
        [HttpGet]
        public HttpResponseMessage GetIncompleteTasks(int moduleId)
        {
            try
            {
                var tasks = new TaskController().GetIncompleteTasks(moduleId).ToJson();
                return Request.CreateResponse(HttpStatusCode.OK, tasks);
            }

            catch (Exception exc)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, exc);
            }

        }

        public class TaskToCreateDTO
        {
            public string TTC_TaskName { get; set; }
            public string TTC_TaskDescription { get; set; }
            public bool TTC_isComplete { get; set; }
            public int TTC_ModuleID { get; set; }
            public int TTC_UserID { get; set; }
        }


        [DnnModuleAuthorize(AccessLevel = SecurityAccessLevel.View)]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public HttpResponseMessage AddTask(TaskToCreateDTO DTO)
        {
            try
            {
                var task = new Task()
                {
                    TaskName = DTO.TTC_TaskName,
                    TaskDescription = DTO.TTC_TaskDescription,
                    isComplete = DTO.TTC_isComplete,
                    ModuleId = DTO.TTC_ModuleID,
                    UserId = DTO.TTC_UserID
                };

                TaskController tc = new TaskController();
                tc.AddTask(task);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception exc)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, exc);
            }
        }

        
        
        public class TaskToUpdateDTO
        {
            public string TTU_TaskName { get; set; }
            public string TTU_TaskDescription { get; set; }
            public bool TTU_isComplete { get; set; }
            public int TTU_TaskID { get; set; }
        }


        [DnnModuleAuthorize(AccessLevel = SecurityAccessLevel.View)]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public HttpResponseMessage UpdateTask(TaskToUpdateDTO DTO)
        {
            try
            {
                var task = new Task()
                {
                    TaskName = DTO.TTU_TaskName,
                    TaskDescription = DTO.TTU_TaskDescription,
                    isComplete = DTO.TTU_isComplete,
                    TaskId = DTO.TTU_TaskID,
                };

                TaskController tc = new TaskController();
                tc.UpdateTask(task);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception exc)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, exc);
            }
        }



        public class TaskToDeleteDTO
        {
            public int TTD_TaskID { get; set; }
        }

        [DnnModuleAuthorize(AccessLevel = SecurityAccessLevel.View)]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public HttpResponseMessage DeleteTask(TaskToDeleteDTO DTO)
        {
            try
            {
                var task = new Task()
                {
                    TaskId = DTO.TTD_TaskID
                };

                TaskController tc = new TaskController();
                tc.DeleteTask(task.TaskId);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception exc)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, exc);
            }
        }





    }
}