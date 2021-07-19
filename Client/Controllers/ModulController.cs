using Client.Models;
using Client.Repository.Data;
using Microsoft.AspNetCore.Mvc;
using ProjectTimeLine.Model;
using ProjectTimeLine.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Controllers
{
    public class ModulController : Controller
    {
        private readonly ModulRepository modulRepository;
        private readonly AccountTaskRepository accountTaskRepository;
        private readonly TaskModulRepository taskModulRepository;

        public ModulController(ModulRepository modulRepository, AccountTaskRepository accountTaskRepository, TaskModulRepository taskModulRepository)
        {
            this.modulRepository = modulRepository;
            this.accountTaskRepository = accountTaskRepository;
            this.taskModulRepository = taskModulRepository;
        }

        public async Task<string> InsertModul(Modul modul)
        {
            var message = await modulRepository.InsertModul(modul);
            return message;
        }
        public async Task<List<Modul>> GetModulView()
        {
            var message = await modulRepository.GetModulView();
            return message;
        }

        public async Task<Modul> GetModulIdView(int id)
        {
            var message = await modulRepository.GetModulIdView(id);
            return message;
        }

        public async Task<List<AccountTask>> GetTaskView()
        {
            var message = await accountTaskRepository.GetTaskView();
            return message;
        }

        public async Task<List<TaskModul>> GetTaskModelView()
        {
            var message = await taskModulRepository.GetTaskModelView();
            return message;
        }

        public async Task<string> InsertAccountTask(AccountTask accountTask)
        {
            var message = await accountTaskRepository.InsertAccountTask(accountTask);
            return message;
        }
        
        public async Task<string> InsertTaskModul(TaskModul taskModul)
        {
            var message = await taskModulRepository.InsertTaskModul(taskModul);
            return message;
        }

        public async Task<ICollection<TaskMemberVM>> GetTaskAccView()
        {
            var result = await taskModulRepository.GetTaskAccView();
            var re = result.OrderBy(x => x.TaskId).ToList();

            for (int i = 0; i < re.Count; i++)
            {
                for (int j = 0; j < re.Count; j++)
                {
                    if (re[i].TaskId == re[j].TaskId)
                    {
                        re[i].Member.Add(re[j].Name);
                    }
                }
            }
            var final = re.GroupBy(p => p.TaskId).Select(grp => grp.First()).ToArray();
            return final;
        }
        public async Task<string> DeleteTaskModul(int id)
        {
            var message = await taskModulRepository.DeleteTaskModul(id);
            return message;
        }
        
        public async Task<string> DeleteModul(int id)
        {
            var message = await modulRepository.DeleteModul(id);
            return message;
        }

        public async Task<string> UpdateModul(Modul modul)
        {
            var message = await modulRepository.UpdateModul(modul);
            return message;
        }
        
        public async Task<string> UpdateTaskModul(TaskModul taskModul)
        {
            var message = await taskModulRepository.UpdateTaskModul(taskModul);
            return message;
        }

        public async Task<string> DeleteMember (int id)
        {
            var message = await taskModulRepository.DeleteMember(id);
            return message;
        }

        public async Task<List<ModulVM>> GetModulTable()
        {
            var message = await modulRepository.GetModulTable();
            return message;
        }
        public async Task<IEnumerable<TaskMemberVM>> GetTaskById(int id)
        {
            var result = await taskModulRepository.GetTaskById(id);
            var re = result.OrderBy(x => x.TaskId).ToList();
            for (int i = 0; i < re.Count; i++)
            {
                for (int j = 0; j < re.Count; j++)
                {
                    if (re[i].TaskId == re[j].TaskId)
                    {
                        re[i].NIKMember.Add(re[j].NIK);
                        re[i].Member.Add(re[j].Name);
                    }
                }
            }
            var final = re.GroupBy(p => p.TaskId).Select(grp => grp.First());
            return final;
        }
    }
}
