using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using System.Linq.Expressions;
using MvcImage.Models;

namespace MvcImage.Models
{

	public class CommonDbContext : DbContext
	{
		public CommonDbContext()
			: base("DefaultConnection")
		{
		}


		public DbSet<Image> Images {get; set;}


	}
}