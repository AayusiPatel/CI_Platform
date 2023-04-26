using CI_Platform.Entities.Data;
using CI_Platform.Entities.Models;
using CI_Platform.Entities.ViewModels;
using CI_Platform.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Helpers;

namespace CI_Platform.Repository.Repository
{
    public class AdminRepository : IAdminRepository
    {
        public readonly CiPlatformContext _db;

        public AdminRepository(CiPlatformContext db)
        {
            _db = db;

        }
        public AdminViewModel displayModel()
        {
            AdminViewModel adminModel = new AdminViewModel();
            {
                adminModel.users = _db.Users.Where(x => x.DeletedAt == null).ToList();
                adminModel.missions = _db.Missions.Where(x => x.DeletedAt == null).ToList();
                adminModel.cms = _db.CmsPages.Where(x => x.DeletedAt == null).ToList();
                adminModel.missionApplications = _db.MissionApplications.Where(x => x.DeletedAt == null).ToList();
                adminModel.stories = _db.Stories.Where(x => x.DeletedAt == null && x.Status != "DRAFT").ToList();
                adminModel.themes = _db.MissionThemes.Where(x => x.DeletedAt == null).ToList();
                adminModel.skills = _db.Skills.Where(x => x.DeletedAt == null).ToList();
                adminModel.Citys = _db.Cities.Where(x => x.DeletedAt == null).ToList();
                adminModel.countries = _db.Countries.Where(x => x.DeletedAt == null).ToList();
                adminModel.Dmissionskills = adminModel.skills;
                adminModel.banners = _db.Banners.Where(x => x.DeletedAt == null).ToList();
            }
            return adminModel;
        }
        public List<Skill> searchSkill(String? obj)
        {
            if (obj != null)
            {
                obj = obj.ToLower();
                List<Skill> skills = _db.Skills
                    .Where(m => m.SkillName.ToLower().Contains(obj) && m.DeletedAt == null)
                    .ToList();
                return skills;
            }
            return _db.Skills.Where(x => x.DeletedAt == null).ToList();
        }
        public List<MissionTheme> searchTheme(String? obj)
        {
            if (obj != null)
            {
                obj = obj.ToLower();
                List<MissionTheme> themes = _db.MissionThemes
                    .Where(m => m.Title.ToLower().Contains(obj) && m.DeletedAt == null)
                    .ToList();
                return themes;
            }
            return _db.MissionThemes.Where(x => x.DeletedAt == null).ToList();
        }
        public List<User> searchUser(String? obj)
        {
            if (obj != null)
            {
                obj = obj.ToLower();
                List<User> users = _db.Users
                    .Where(m => m.FirstName.ToLower().Contains(obj) || m.LastName.ToLower().Contains(obj) && m.DeletedAt == null)
                    .ToList();
                return users;
            }
            return _db.Users.Where(x => x.DeletedAt == null).ToList();
        }

        public List<CmsPage> searchCms(String? obj)
        {
            if (obj != null)
            {
                obj = obj.ToLower();
                List<CmsPage> cms = _db.CmsPages
                    .Where(m => m.Title.ToLower().Contains(obj) && m.DeletedAt == null)
                    .ToList();
                return cms;
            }
            return _db.CmsPages.Where(x => x.DeletedAt == null).ToList();
        }
        public List<Banner> searchBanner(String? obj)
        {
            //if (obj != null)
            //{
            //    obj = obj.ToLower();
            //    List<Banner> banner = _db.Banners
            //        .Where(m => m.Text.ToString().Contains(obj) && m.DeletedAt == null)
            //        .ToList();
            //    return banner;
            //}
            return _db.Banners.Where(x => x.DeletedAt == null).ToList();
        }

        public List<Mission> searchMission(String? obj)
        {
            if (obj != null)
            {
                obj = obj.ToLower();
                List<Mission> missions = _db.Missions
                    .Where(m => m.Title.ToLower().Contains(obj) && m.DeletedAt == null)
                    .ToList();
                return missions;
            }
            return _db.Missions.Where(x => x.DeletedAt == null).ToList();
        }

        public List<MissionApplication> searchMissionApplication(String? obj)
        {
            if (obj != null)
            {
                obj = obj.ToLower();
                List<MissionApplication> missionApplication = _db.MissionApplications.Include(m => m.Mission).Include(m => m.User)
                    .Where(m => m.Mission.Title.ToLower().Contains(obj) || m.User.LastName.ToLower().Contains(obj) || m.User.FirstName.ToLower().Contains(obj) && m.DeletedAt == null)
                    .ToList();

                return missionApplication;
            }
            return _db.MissionApplications.Include(m => m.Mission).Include(m => m.User).Where(x => x.DeletedAt == null).ToList();
        }
        public List<Story> searchStory(String? obj)
        {
            if (obj != null)
            {
                obj = obj.ToLower();
                List<Story> story = _db.Stories.Include(m => m.Mission).Include(m => m.User)
                    .Where(m => m.Title.ToLower().Contains(obj) || m.Mission.Title.ToLower().Contains(obj) || m.User.LastName.ToLower().Contains(obj) || m.User.FirstName.ToLower().Contains(obj))
                    .ToList();
                story = story.Where(m => m.DeletedAt == null && m.Status != "DRAFT").ToList();
                return story;
            }
            return _db.Stories.Include(m => m.Mission).Include(m => m.User).Where(x => x.DeletedAt == null && x.Status != "DRAFT").ToList();
        }

        public AdminViewModel EditForm(int id, int page)
        {
            AdminViewModel am = new AdminViewModel();
            am.Citys = _db.Cities.Where(x => x.DeletedAt == null).ToList();
            am.countries = _db.Countries.Where(x => x.DeletedAt == null).ToList();
            am.themes = _db.MissionThemes.Where(x => x.DeletedAt == null).ToList();
            am.Dmissionskills = _db.Skills.Where(x => x.DeletedAt == null).ToList();
            if (page == 1)
            {
                {
                    am.adminUser = _db.Users.FirstOrDefault(x => x.UserId == id && x.DeletedAt == null);
                   if(am.adminUser != null)
                    am.adminUser.Password = null;
                }
            }
            if (page == 2)
            {
                {
                    am.cmsPage = _db.CmsPages.FirstOrDefault(x => x.CmsPageId == id && x.DeletedAt == null);
                }
            }
            if (page == 3)
            {
                {
                    am.mission = _db.Missions.Include(x => x.MissionSkills).Include(x => x.MissionMedia).FirstOrDefault(x => x.MissionId == id && x.DeletedAt == null);
                   
                }
            }
            if (page == 6)
            {
                {
                    am.missionTheme = _db.MissionThemes.FirstOrDefault(x => x.MissionThemeId == id && x.DeletedAt == null);
                }
            }
            if (page == 7)
            {
                {
                    am.skill = _db.Skills.FirstOrDefault(x => x.SkillId == id && x.DeletedAt == null);
                }
            }
            if (page == 8)
            {
                {
                    am.banner = _db.Banners.FirstOrDefault(x => x.BannerId == id && x.DeletedAt == null);
                }
            }
            return am;
        }

        public bool ApplicationApproval(int id, int status)
        {
            MissionApplication ma = _db.MissionApplications.FirstOrDefault(x => x.MissionApplicationId == id && x.DeletedAt == null);
            if (ma == null)
            {
                return false;
            }
            if (ma != null && status == 1)
            {
                ma.ApprovalStatus = "Approve";
            }
            if (ma != null && status == 0)
            {
                ma.ApprovalStatus = "Decline";
            }
            _db.MissionApplications.Update(ma);
            _db.SaveChanges();
            return true;

        }
        public bool StoryApproval(int id, int status)
        {
            Story ma = _db.Stories.FirstOrDefault(x => x.StoryId == id && x.DeletedAt == null);
            if (ma == null)
            {
                return false;
            }
            if (ma != null && status == 1)
            {
                ma.Status = "PUBLISHED";
            }
            if (ma != null && status == 0)
            {
                ma.Status = "DECLINED";
            }
            _db.Stories.Update(ma);
            _db.SaveChanges();
            return true;

        }

        public bool AddCms(AdminViewModel obj)
        {
            if (obj.cmsPage.CmsPageId == 0)
            {
                CmsPage cp = new CmsPage();
                {
                    cp.Title = obj.cmsPage.Title;
                    cp.Description = obj.cmsPage.Description;
                    cp.Slug = obj.cmsPage.Slug;
                }
                _db.CmsPages.Add(cp);
                _db.SaveChanges();
                return true;
            }
            if (obj.cmsPage.CmsPageId != 0)
            {

                CmsPage cp = _db.CmsPages.FirstOrDefault(x => x.CmsPageId == obj.cmsPage.CmsPageId && x.DeletedAt == null);
                {
                    cp.Title = obj.cmsPage.Title;
                    cp.Description = obj.cmsPage.Description;
                    cp.Slug = obj.cmsPage.Slug;
                    cp.UpdatedAt = DateTime.Now;
                }
                _db.CmsPages.Update(cp);
                _db.SaveChanges();
                return true;
            }
            return false;
        }
        public bool AddBanner(AdminViewModel obj)
        {
            if (obj.banner.BannerId == 0)
            {
                Banner banner = new Banner();
                {
                    banner.Text = obj.banner.Text;
                    banner.Image = "DCBJHBEFVEWUB.png";
                }
                _db.Banners.Add(banner);
                _db.SaveChanges();
                return true;
            }
            if (obj.banner.BannerId != 0)
            {

                Banner banner = _db.Banners.FirstOrDefault(x => x.BannerId == obj.banner.BannerId && x.DeletedAt == null);
                {
                    banner.Text = obj.banner.Text;
                    banner.UpdatedAt = DateTime.Now;
                }
                _db.Banners.Update(banner);
                _db.SaveChanges();
                return true;
            }
            return false;
        }
        public bool AddTheme(AdminViewModel obj)
        {
            if (obj.missionTheme.MissionThemeId == 0)
            {
                MissionTheme cp = new MissionTheme();
                {
                    cp.Title = obj.missionTheme.Title;
                    cp.Status = obj.missionTheme.Status;

                }
                _db.MissionThemes.Add(cp);
                _db.SaveChanges();
                return true;
            }
            if (obj.missionTheme.MissionThemeId != 0)
            {

                MissionTheme cp = _db.MissionThemes.FirstOrDefault(x => x.MissionThemeId == obj.missionTheme.MissionThemeId && x.DeletedAt == null);
                {
                    cp.Title = obj.missionTheme.Title;
                    cp.Status = obj.missionTheme.Status;
                    cp.UpdatedAt = DateTime.Now;
                }
                _db.MissionThemes.Update(cp);
                _db.SaveChanges();
                return true;
            }
            return false;
        }
        public bool AddMission(AdminViewModel obj)
        {
            if (obj.mission.MissionId == 0)
            {
                Mission mis = new Mission();
                {
                    mis.Title = obj.mission.Title;
                    mis.ShortDescription = obj.mission.ShortDescription;
                    mis.Description = obj.mission.Description;
                    mis.CountryId = obj.mission.CountryId;
                    mis.CityId = obj.mission.CityId;
                    mis.OrganizationName = obj.mission.OrganizationName;
                    mis.OrganizationDetail = obj.mission.OrganizationDetail;
                    mis.StartDate = obj.mission.StartDate;
                    mis.EndDate = obj.mission.EndDate;
                    mis.MissionType = obj.mission.MissionType;                    
                    mis.ThemeId = obj.mission.ThemeId;
                    mis.Avaibility = obj.mission.Avaibility;

                }
                _db.Missions.Add(mis);
                _db.SaveChanges();
                //Mission mis = _db.Missions.FirstOrDefault(m => m.Title && )
                foreach (var item in obj.missionSkills)
                {

                    MissionSkill misSkill = new MissionSkill();
                    misSkill.MissionId = mis.MissionId;
                    misSkill.SkillId = item;

                    _db.MissionSkills.Add(misSkill);
                    _db.SaveChanges();
                }
                if (obj.missiondoc != null)
                {
                    foreach (var item in obj.missiondoc)
                    {
                        MissionDocument md = new MissionDocument();
                        {
                            md.MissionId = mis.MissionId;
                            md.DocumentName = item.Name;
                            md.DocumentType = item.ContentType;
                            md.DocumentPath = item.FileName;

                        }
                        _db.MissionDocuments.Add(md);
                        _db.SaveChanges();
                        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/Story", item.FileName); //we are using Temp file name just for the example. Add your own file path.

                        if (File.Exists(filePath) == false)
                        {


                            using (var stream = new FileStream(filePath, FileMode.Create))
                            {
                                item.CopyToAsync(stream);
                            }
                        }
                    }
                }
                if (obj.missionMedia != null)
                {
                    foreach (var item in obj.missionMedia)
                    {
                        MissionMedium md = new MissionMedium();
                        {
                            md.MissionId = mis.MissionId;
                            md.MediaName = item.Name;
                            md.MediaType = item.ContentType;
                            md.MediaPath = item.FileName;

                        }
                        _db.MissionMedia.Add(md);
                        _db.SaveChanges();
                        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/Story", item.FileName); //we are using Temp file name just for the example. Add your own file path.

                        if (File.Exists(filePath) == false)
                        {


                            using (var stream = new FileStream(filePath, FileMode.Create))
                            {
                                item.CopyToAsync(stream);
                            }
                        }
                    }
                }
                if(obj.url != null)
                {
                    MissionMedium md = new MissionMedium();
                    {
                        md.MissionId = mis.MissionId;
                        md.MediaName = "missionUrl";
                        md.MediaType = "Url";
                        md.MediaPath = obj.url;

                    }
                    _db.MissionMedia.Add(md);
                    _db.SaveChanges();

                }
                return true;
            }
            if (obj.mission.MissionId != 0)
            {

                Mission mis = _db.Missions.FirstOrDefault(x => x.MissionId == obj.mission.MissionId && x.DeletedAt == null);

                {
                    mis.Title = obj.mission.Title;
                    mis.ShortDescription = obj.mission.ShortDescription;
                    mis.Description = obj.mission.Description;
                    mis.CountryId = obj.mission.CountryId;
                    mis.CityId = obj.mission.CityId;
                    mis.OrganizationName = obj.mission.OrganizationName;
                    mis.OrganizationDetail = obj.mission.OrganizationDetail;
                    mis.StartDate = obj.mission.StartDate;
                    mis.EndDate = obj.mission.EndDate;
                    mis.MissionType = obj.mission.MissionType;                
                    mis.ThemeId = obj.mission.ThemeId;
                    mis.Avaibility = obj.mission.Avaibility;
                    mis.UpdatedAt = DateTime.Now;


                }
                _db.Missions.Update(mis);
                _db.SaveChanges();
               

              
                if (obj.missionSkills.Count > 0)
                {
                    List<MissionSkill> skills = _db.MissionSkills.Where(x => x.MissionId == mis.MissionId && x.DeletedAt == null).ToList();
                    _db.RemoveRange(skills);
                    foreach (var item in obj.missionSkills)
                    {

                        MissionSkill misSkill = new MissionSkill();
                        misSkill.MissionId = mis.MissionId;
                        misSkill.SkillId = item;

                        _db.MissionSkills.Add(misSkill);
                        _db.SaveChanges();
                    }
                }
                List<MissionDocument> docs = _db.MissionDocuments.Where(x => x.MissionId == mis.MissionId && x.DeletedAt == null).ToList();
                if (docs.Count > 0)
                {
                    foreach (var doc in docs)
                    {
                        doc.DeletedAt = DateTime.Now;
                    }
                    _db.UpdateRange(docs);
                    _db.SaveChanges();
                }
                if (obj.missiondoc != null)
                {
                    foreach (var item in obj.missiondoc)
                    {
                        MissionDocument md = new MissionDocument();
                        {
                            md.MissionId = mis.MissionId;
                            md.DocumentName = item.Name;
                            md.DocumentType = item.ContentType;
                            md.DocumentPath = item.FileName;

                        }
                        _db.MissionDocuments.Add(md);
                        _db.SaveChanges();
                        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/Story", item.FileName); //we are using Temp file name just for the example. Add your own file path.

                        if (File.Exists(filePath) == false)
                        {


                            using (var stream = new FileStream(filePath, FileMode.Create))
                            {
                                item.CopyToAsync(stream);
                            }
                        }
                    }
                }
                List<MissionMedium> mism = _db.MissionMedia.Where(x => x.MissionId == mis.MissionId && x.DeletedAt == null ).ToList();
                if (mism != null)
                {
                    foreach (var media in mism)
                    {
                        media.DeletedAt = DateTime.Now;
                    }
                    _db.UpdateRange(mism);
                    _db.SaveChanges();
                }
                if (obj.missionMedia.Count > 0)
                {
                    foreach (var item in obj.missionMedia)
                    {
                        MissionMedium md = new MissionMedium();
                        {
                            md.MissionId = mis.MissionId;
                            md.MediaName = item.Name;
                            md.MediaType = "img";
                            md.MediaPath = item.FileName;

                        }
                        _db.MissionMedia.Add(md);
                        _db.SaveChanges();
                        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/Story", item.FileName); //we are using Temp file name just for the example. Add your own file path.

                        if (File.Exists(filePath) == false)
                        {


                            using (var stream = new FileStream(filePath, FileMode.Create))
                            {
                                item.CopyToAsync(stream);
                            }
                        }
                    }
                }
                if (obj.url != null)
                {
                    MissionMedium md = _db.MissionMedia.FirstOrDefault(x => x.MissionId== mis.MissionId && x.MediaType == "Url");
                    //_db.MissionMedia.Remove(md);
                        if(md == null)
                    {
                        md= new MissionMedium();
                    }
                    {
                        md.MissionId = mis.MissionId;
                        md.MediaName = "missionUrl";
                        md.MediaType = "Url";
                        md.MediaPath = obj.url;
                        md.UpdatedAt = DateTime.Now;

                    }
                    _db.MissionMedia.Update(md);
                    _db.SaveChanges();

                }
                return true;
            }
            return false;
        }

        public bool AddSkill(AdminViewModel obj)
        {
            if (obj.skill.SkillId == 0)
            {
                Skill cp = new Skill();
                {
                    cp.SkillName = obj.skill.SkillName;
                    cp.Status = obj.skill.Status;

                }
                _db.Skills.Add(cp);
                _db.SaveChanges();
                return true;
            }
            if (obj.skill.SkillId != 0)
            {

                Skill cp = _db.Skills.FirstOrDefault(x => x.SkillId == obj.skill.SkillId && x.DeletedAt == null);
                {
                    cp.SkillName = obj.skill.SkillName;
                    cp.Status = obj.skill.Status;
                    cp.UpdatedAt = DateTime.Now;
                }
                _db.Skills.Update(cp);
                _db.SaveChanges();
                return true;
            }
            return false;
        }
        public bool DeleteActivity(int id, int page)
        {
            if (page == 1)
            {
                User user = _db.Users.FirstOrDefault(m => m.UserId == id && m.DeletedAt == null);
                if (user != null)
                {
                    user.DeletedAt = DateTime.Now;
                    _db.Update(user);
                    _db.SaveChanges();

                    return true;
                }
                if (user == null)
                {
                    return false;
                }
            }
            if (page == 2)
            {
                CmsPage cmsPage = _db.CmsPages.FirstOrDefault(m => m.CmsPageId == id && m.DeletedAt == null);
                if (cmsPage != null)
                {
                    cmsPage.DeletedAt = DateTime.Now;
                    _db.Update(cmsPage);
                    _db.SaveChanges();

                    return true;
                }
                if (cmsPage == null)
                {
                    return false;
                }
            }
            if (page == 5)
            {
                Story story = _db.Stories.FirstOrDefault(m => m.StoryId == id && m.DeletedAt == null);
                if (story != null)
                {
                    story.DeletedAt = DateTime.Now;
                    _db.Update(story);
                    _db.SaveChanges();

                    return true;
                }
                if (story == null)
                {
                    return false;
                }
            }
            if (page == 6)
            {
                MissionTheme theme = _db.MissionThemes.FirstOrDefault(m => m.MissionThemeId == id && m.DeletedAt == null);
                if (theme != null)
                {
                    theme.DeletedAt = DateTime.Now;
                    _db.Update(theme);
                    _db.SaveChanges();

                    return true;
                }
                if (theme == null)
                {
                    return false;
                }
            }
            if (page == 7)
            {
                Skill skill = _db.Skills.FirstOrDefault(m => m.SkillId == id && m.DeletedAt == null);
                if (skill != null)
                {
                    skill.DeletedAt = DateTime.Now;
                    _db.Update(skill);
                    _db.SaveChanges();

                    return true;
                }
                if (skill == null)
                {
                    return false;
                }
            }
            if (page == 8)
            {
                Banner banner = _db.Banners.FirstOrDefault(m => m.BannerId == id && m.DeletedAt == null);
                if (banner != null)
                {
                    banner.DeletedAt = DateTime.Now;
                    _db.Update(banner);
                    _db.SaveChanges();

                    return true;
                }
                if (banner == null)
                {
                    return false;
                }
            }
            return false;
        }

        public bool AddUser(AdminViewModel obj)
        {
            if (obj.Avatarfile != null)
            {
                
                // full path to file in temp location
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/Story", obj.Avatarfile.FileName); //we are using Temp file name just for the example. Add your own file path.

                if (File.Exists(filePath) == false)
                {

                    
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        obj.Avatarfile.CopyToAsync(stream);
                    }
                }

            }
            if (obj.adminUser.UserId == 0)
            {
                User profile = new User();
                {
                    profile.FirstName = obj.adminUser.FirstName;
                    profile.LastName = obj.adminUser.LastName;
                    profile.Email = obj.adminUser.Email;
                    profile.Password = Crypto.HashPassword(obj.adminUser.Password);
                    profile.EmployeeId = obj.adminUser.EmployeeId;
                    profile.Department = obj.adminUser.Department;
                    profile.ProfileText = obj.adminUser.ProfileText;
                    profile.CountryId = obj.adminUser.CountryId;
                    profile.CityId = obj.adminUser.CityId;
                    profile.Status = obj.adminUser.Status;
                    if (obj.Avatarfile != null)
                        profile.Avatar = obj.Avatarfile.FileName;
                }
                _db.Users.Add(profile);
                _db.SaveChanges();
                return true;
            }
            if (obj.adminUser.UserId != 0)
            {

                User profile = _db.Users.FirstOrDefault(x => x.UserId == obj.adminUser.UserId && x.DeletedAt == null);
                {
                    profile.FirstName = obj.adminUser.FirstName;
                    profile.LastName = obj.adminUser.LastName;
                    profile.Email = obj.adminUser.Email;
                    profile.Password = Crypto.HashPassword(obj.adminUser.Password);
                    profile.EmployeeId = obj.adminUser.EmployeeId;
                    profile.Department = obj.adminUser.Department;
                    profile.ProfileText = obj.adminUser.ProfileText;
                    profile.CountryId = obj.adminUser.CountryId;
                    profile.CityId = obj.adminUser.CityId;
                    profile.Status = obj.adminUser.Status;
                    profile.UpdatedAt = DateTime.Now;
                    if (obj.Avatarfile != null)
                        profile.Avatar = obj.Avatarfile.FileName;
                }
                _db.Users.Update(profile);
                _db.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
