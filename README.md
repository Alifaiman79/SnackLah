# SnackLah

SnackLah is a small food ordering application built with ASP.NET Web Forms, C# and SQL Server LocalDB. Customers can browse the menu, add items to a session cart, apply a promo code and place an order. An admin page lists orders and manages user accounts.

## Stack

- ASP.NET Web Forms on .NET Framework 4.7.2
- C# and ADO.NET
- SQL Server LocalDB
- HTML and CSS

## Project layout

- `SnackLah/` — pages, code behind files, styles and images
- `SQLQuery1.sql` and `SQLQuery2.sql` — sample product data updates
- `SnackLah.sln` — Visual Studio solution

## Running locally

Open `SnackLah.sln` in Visual Studio with the ASP.NET and web development workload and SQL Server LocalDB installed. Restore NuGet packages, create a LocalDB database at `SnackLah/App_Data/SnackLahDB.mdf`, and confirm the connection string in `SnackLah/Web.config`. The repository does not include the database or a complete schema creation script, so the app will not run from a fresh clone without the original schema and seed data.

## Current limitations

This is a coursework project, not a production storefront. Passwords are currently stored as plain text in the database; account data should not be real. Before deployment, migrate passwords to a salted password hash, reset existing passwords, and review checkout consistency and order totals. Promo discounts are shown in the cart but are not stored with orders. No automated test suite or CI workflow is included.

The `portfolio-fixes` review patch restricts the user management page to admins, removes password display and editing from that page, and encodes user supplied text rendered in the welcome message and order history.
