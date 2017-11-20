# MVC-SQL-ASP.NET-Web-Application
Basic MVC-SQL-ASP.NET-Web-Application

This assignment uses an MVC service called MVC-SQL, which retrieves data from three SQL tables, Categories, Product sand Suppliers, from a slightly modified copy of the public Northwind SQL Server database sample, assumed to be located in C:\usertmp\.
MVC-SQL returns readable HTML pages containing a read-only server-side paginated grid view(Webgrid), directly accessible via a browser.

Model: Northwind.mdf into MyModel.edmx/MyModel.cs 
View: Webgrid.cshtml is a cshtml file with the webgrid on it, with assocaited formatting and styling.
Controller: SQL controller formats all of the data from the SQL talbes into a suitable format for the webgrid while also dealing with potential null values.

