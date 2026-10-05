using GiveAid.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    ));
builder.Services.AddDefaultIdentity<IdentityUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
})
.AddRoles<IdentityRole>()
.AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddRazorPages();

var app = builder.Build();

// Role creation code here

using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    string[] roles = { "Admin", "User" };
    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole(role));
        }
    }

    // Seed Default Admin User
    var adminEmail = "admin@giveaid.org";
    var adminUser = await userManager.FindByEmailAsync(adminEmail);
    if (adminUser == null)
    {
        adminUser = new IdentityUser
        {
            UserName = adminEmail,
            Email = adminEmail,
            EmailConfirmed = true
        };
        var createResult = await userManager.CreateAsync(adminUser, "Admin@123");
        if (createResult.Succeeded)
        {
            await userManager.AddToRoleAsync(adminUser, "Admin");
        }
    }

    // Seed Default Regular User
    var userEmail = "user@giveaid.org";
    var regularUser = await userManager.FindByEmailAsync(userEmail);
    if (regularUser == null)
    {
        regularUser = new IdentityUser
        {
            UserName = userEmail,
            Email = userEmail,
            EmailConfirmed = true
        };
        var createResult = await userManager.CreateAsync(regularUser, "User@123");
        if (createResult.Succeeded)
        {
            await userManager.AddToRoleAsync(regularUser, "User");
        }
    }

    // Seed Causes if empty
    if (!dbContext.Causes.Any())
    {
        dbContext.Causes.AddRange(
            new GiveAid.Models.Cause { CauseName = "Children Welfare & Nutrition" },
            new GiveAid.Models.Cause { CauseName = "Disabled & Specially Challenged Support" },
            new GiveAid.Models.Cause { CauseName = "Free Education & Orphan Sponsorship" },
            new GiveAid.Models.Cause { CauseName = "Community Free Healthcare Camps" },
            new GiveAid.Models.Cause { CauseName = "Women Vocational Training & Empowerment" }
        );
        await dbContext.SaveChangesAsync();
    }

    // Seed NGOs if empty
    if (!dbContext.NGOs.Any())
    {
        var ngo1 = new GiveAid.Models.Ngo { NgoName = "Hope Child Welfare Trust", Description = "Dedicated to education, nutrition, and psychological support for underprivileged children." };
        var ngo2 = new GiveAid.Models.Ngo { NgoName = "Apex Health & Relief Society", Description = "Conducts free specialized diagnostic, eye care, and surgical camps in rural regions." };
        var ngo3 = new GiveAid.Models.Ngo { NgoName = "EmpowerAbility Alliance", Description = "Empowers specially-abled individuals with prosthetics, wheelchairs, and job skills." };

        dbContext.NGOs.AddRange(ngo1, ngo2, ngo3);
        await dbContext.SaveChangesAsync();

        // Seed Programmes linked to NGOs
        if (!dbContext.Programmes.Any())
        {
            dbContext.Programmes.AddRange(
                new GiveAid.Models.Programme { ProgrammeName = "Rural Free Pediatric Health Camp 2026", NgoId = ngo2.Id },
                new GiveAid.Models.Programme { ProgrammeName = "Annual Orphan School Stationery Drive", NgoId = ngo1.Id },
                new GiveAid.Models.Programme { ProgrammeName = "Wheelchair & Assistive Device Distribution", NgoId = ngo3.Id }
            );
            await dbContext.SaveChangesAsync();
        }
    }

    // Seed Galleries if empty
    if (!dbContext.Galleries.Any())
    {
        dbContext.Galleries.AddRange(
            new GiveAid.Models.Gallery { Title = "Free Education & Stationery Drive", ImagePath = "https://images.unsplash.com/photo-1488521787991-ed7bbaae773c?q=80&w=600" },
            new GiveAid.Models.Gallery { Title = "Community Free Health & Diagnostic Camp", ImagePath = "https://images.unsplash.com/photo-1576765608535-5f04d1e3f289?q=80&w=600" },
            new GiveAid.Models.Gallery { Title = "Emergency Food Ration Distribution", ImagePath = "https://images.unsplash.com/photo-1593113598332-cd288d649433?q=80&w=600" }
        );
        await dbContext.SaveChangesAsync();
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

// app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();
app.MapRazorPages();

app.Run();
