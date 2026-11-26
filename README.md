# SnackLah

SnackLah is a food ordering application built with ASP.NET Web Forms, C# and SQL Server LocalDB. Customers can browse the menu, add items to a session cart, apply a promo code and place an order. An admin page lists orders and manages user accounts.

## Stack

- ASP.NET Web Forms on .NET Framework 4.7.2
- C# and ADO.NET
- SQL Server LocalDB
- HTML and CSS

## Project layout

- `SnackLah/` — pages, code behind files, styles and images
- `database/001-password-hash-column.sql` — password column migration
- `SQLQuery1.sql` and `SQLQuery2.sql` — sample product data updates
- `SnackLah.sln` — Visual Studio solution

## Running locally

Open `SnackLah.sln` in Visual Studio with the ASP.NET and web development workload and SQL Server LocalDB installed. Restore NuGet packages, create a LocalDB database at `SnackLah/App_Data/SnackLahDB.mdf`, and confirm the connection string in `SnackLah/Web.config`. The repository does not include the database or a complete schema creation script, so the app will not run from a fresh clone without the original schema and seed data.

## Password migration

Back up the existing database and run `database/001-password-hash-column.sql` before deploying this code. Registration stores new passwords as salted PBKDF2-SHA256 hashes. On a successful login, an existing plain-text password is replaced with a hash. Accounts that never log in retain their old plain-text values; reset those passwords before using real account data. Test registration, login and an existing account against a copy of the original database before deployment.

## Current limitations

This is a coursework project, not a production storefront. The repository has no complete database schema, automated test suite or CI workflow. Review checkout consistency and order totals before deployment. Promo discounts are shown in the cart but are not stored with orders.
