﻿using MVCBlog.Entities.Models;
using MVCBlog.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
namespace MVCBlog.Web.CommonHelper
{
    public class Helper
    {
        public static List<SelectListItem> GetCategorySelectList()
        {
            CategoryService service = (CategoryService)ResolverHelper.GetResolver<CategoryService>();
            List<CategoryInfo> list = service.GetCategoryList();
            if (list.Count == 0)
            {
                UserService userservice = (UserService)ResolverHelper.GetResolver<UserService>();
                var loginuser = UserHelper.GetLogInUserInfo();
                var defaultcategory = new CategoryInfo()
                {
                    Id = 0,
                    CategoryName = "未分类",
                    CreateTime = DateTime.Now,
                    CreateUser = loginuser,
                    IsDelete = false
                };
                service.AddCategoryInfo(defaultcategory);
                return GetCategorySelectList();
            }
            var res = list.Select(x => new SelectListItem() { Text = x.CategoryName, Value = x.Id.ToString() }).ToList();
            return res;
        }
    }
}