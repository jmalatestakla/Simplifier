# Foodbank Application Simplifier
CRUD app used to help me learn the stack :D 


### User Story
IDK how food bank applications actually work, but for the sake of scope we are just going to assume everything is either a txt file or just a chunk of text.

- User joins app through auth (out of scope here, just make single user for now.)
- They must first make a template (being how they want their text to be output)
- They can then go to applications and add a new application
  - They can drag and drop a txt file or copy and paste into a field. 
  - Select a template from a dropdown of templates they have created
  - Submit those two fields and begin loading.
  - It will automatically fill out the expected fields and save that as the formResponses
- Application and template data is saved and can be viewed, edited, and deleted in their own tabs.


### Schema
- User
  - uuid (UUID)
  - email (String)
- Application
  - uuid (UUID, primary)
  - user_id (UUID, foreign to User table)
  - name (String, how it will be easily identified)
  - raw_text (Text)
  - created_at (Timestamp)
  - updated_at (Timestamp)
  - template_id (UUID, foreign to Template table)
  - form_responses (JSON, must match form_fields in template)
- Template
  - uuid (UUID, primary)
  - user_id (UUID, foreign to User table)
  - name (String)
  - form_fields (JSON)
  - created_at (Timestamp)
  - updated_at (Timestamp)

### CRUD
- Allow applications and templates to be created, edited, and deleted
- Read list of applications and t

### Setup commands
- Run angular
  - cd Simplifier/ClientApp
  - npm run start
- Run .Net backend
  - cd Simplifier
  - dotnet run
- Update DB Schema
  - Change Schema in Models folder
  - cd Simplifier.Entities
  - dotnet ef migrations add YourMigrationName --startup-project ../Simplifier/Simplifier.csproj
  - dotnet ef database update --startup-project ../Simplifier/Simplifier.csproj    


# What I got through
- Full CRUD operations for User
- Create and read for templates

# With some more time
- Actually getting AI portion to work.
- Finish rest of crud apps

# Difficulties
- EF Setup and Migrations