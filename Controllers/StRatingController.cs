using Examcy.Data;
using Examcy.Data.Models;
using Examcy.Data.Repository;
using Examcy.ViewModels.Student;
using Microsoft.AspNetCore.Mvc;

namespace Examcy.Controllers
{
    public class StRatingController : Controller
    {
        //AppDBContent db;
        //public StRatingController(AppDBContent context)
        //{
        //    db = context;
        //}

        public IActionResult Index(int id)
        {
            UserRepository u = new UserRepository();
            int idU = u.GetUserIdByLogin(User.Identity.Name);// id пользователя
            if (idU != 0)
            {
                ViewBag.UserName = User.Identity.Name;
                int idS = u.GetStudentIdByUserId(idU); // id студента
                int idC = id; // id курса

                RatingRepository rt = new RatingRepository();


                StRatingViewModel model = new StRatingViewModel();

                var ratings = rt.AllRating(idC);

                var testratings = rt.TestRating(idC);

                int max = 0;
                int me = 0;
                foreach (Rating r in ratings)
                {
                    if (r.ExpAll > max)
                        max = r.ExpAll;
                    if (r.numberAllRairing == idS)
                        me = r.ExpAll;
                }
                model.n_allraiting = max - me;

                max = 0;
                me = 0;
                foreach (Rating r in testratings)
                {
                    if (r.ExpTestVar > max)
                        max = r.ExpTestVar;
                    if (r.numberTestRairing == idS)
                        me = r.ExpTestVar;
                }
                model.n_testraiting = max - me;

                max = 0;
                me = 0;
                foreach (Rating r in ratings)
                {
                    if (r.Visitors > max)
                        max = r.Visitors;
                    if (r.numberVisitorsRairing == idS)
                        me = r.Visitors;
                }
                model.n_visitors = max - me;



                model.allratings.AddRange(from r in ratings
                                          orderby r.ExpAll descending
                                          select r);
                int n = 1;
                foreach (Rating r in model.allratings)
                {
                    r.numberAllRairing = n;
                    n++;
                }
                

                model.testratings.AddRange(from r in testratings
                                          orderby r.ExpTestVar descending
                                           select r);

                n = 1;
                foreach (Rating r in model.testratings)
                {
                    r.numberTestRairing = n;
                    n++;
                }

      


                model.visitors.AddRange(from r in ratings
                                          orderby r.Visitors descending
                                        select r);

                n = 1;
                foreach (Rating r in model.visitors)
                {
                    r.numberVisitorsRairing = n;
                    n++;
                }

                
                int i = Convert.ToInt32( Math.Round( model.allratings.Count * 0.25, 0, MidpointRounding.AwayFromZero));

                if (model.allratings.Count != 0)
                {
                    model.first_allraiting = model.allratings[i].ExpAll;


                    int summ = 0;
                    foreach (var r in model.allratings)
                    {
                        summ += r.ExpAll;
                    }
                    summ = summ / model.allratings.Count;

                    i = 10000000;

                    foreach (var r in model.allratings)
                    {
                        if (summ <= r.ExpAll && r.ExpAll <= model.first_allraiting)
                        {
                            i = r.ExpAll;
                        }
                    }
                    model.second_allraiting = i;
                }

                i = Convert.ToInt32(Math.Round(model.testratings.Count * 0.25, 0, MidpointRounding.AwayFromZero));

                if (model.testratings.Count != 0)
                {

                    model.first_testraiting = model.testratings[i].ExpTestVar;

                    //i = Convert.ToInt32(Math.Round(model.testratings.Count * 0.5, 0, MidpointRounding.AwayFromZero));

                    //model.second_testraiting = model.testratings[i].ExpTestVar;

                    int summ = 0;
                    foreach (var r in model.testratings)
                    {
                        summ += r.ExpTestVar;
                    }
                    summ = summ / model.testratings.Count;

                    i = 10000000;

                    foreach (var r in model.testratings)
                    {
                        if (summ <= r.ExpTestVar && r.ExpTestVar <= model.first_testraiting)
                        {
                            i = r.ExpTestVar;
                        }
                    }
                    if (i == 10000000) i = 0;
                    model.second_testraiting = i;
                }


                i = Convert.ToInt32(Math.Round(model.visitors.Count * 0.25, 0, MidpointRounding.AwayFromZero));

                if (model.visitors.Count != 0)
                {

                    model.first_visitors = model.visitors[i].Visitors;

                    //i = Convert.ToInt32(Math.Round(model.visitors.Count * 0.5, 0, MidpointRounding.AwayFromZero));

                    //model.second_visitors = model.visitors[i].Visitors;

                    int summ = 0;
                    foreach (var r in model.visitors)
                    {
                        summ += r.Visitors;
                    }
                    summ = summ / model.visitors.Count;

                    i = 10000000;

                    foreach (var r in model.visitors)
                    {
                        if (summ <= r.Visitors && r.Visitors <= model.first_visitors)
                        {
                            i = r.Visitors;
                        }
                    }
                    model.second_visitors = i;
                }


                try
                {
                    AssignedVarsRepository assignedVarsRepository = new AssignedVarsRepository();
                    model.idVar = assignedVarsRepository.getTestVar(idS, idC);
                }
                catch (Exception ex)
                {
                    model.idVar = 0;
                }
                ViewBag.Title = "Рейтинг | Examcy";
                return View(model);
            }
            return Redirect("~/Home/Index");
        }
    }
}
