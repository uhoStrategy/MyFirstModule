using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections.Generic;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Data;


namespace Christoc.Modules.MyFirstModule.Models
{
    public class TaskController
    {
        public IList<Task> GetTasks(int ModuleId)
        {
            return CBO.FillCollection<Task>(DataProvider.Instance().ExecuteReader("CBP_GetTasks", ModuleId));
        }


        public IList<Task> GetIncompleteTasks(int ModuleId)
        {
            return CBO.FillCollection<Task>(DataProvider.Instance().ExecuteReader("CBP_GetIncompleteTasks", ModuleId));
        }



       
        public void AddTask(Task task)
        {
            task.TaskId = DataProvider.Instance().ExecuteScalar<int>("CBP_AddTask",
                                                      task.TaskName,
                                                      task.TaskDescription,
                                                      task.isComplete,
                                                      task.ModuleId,
                                                      task.UserId
                                                        );
        }



        public void UpdateTask(Task task)
        {
            task.TaskId = DataProvider.Instance().ExecuteScalar<int>("CBP_UpdateTask",
                                                      task.TaskId,
                                                      task.TaskName,
                                                      task.TaskDescription,
                                                      task.isComplete
                                                     );
        }


        
        public void DeleteTask(int taskId)
        {
            DataProvider.Instance().ExecuteNonQuery("CBP_DeleteTask", taskId);
        }

       

    }
}