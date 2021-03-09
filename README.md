### Exercise-15 Step Description

Estimated efforts reuiqred to complete this exercise is 1 hour.

In this exercise, we will implement JWT (JSON Web Token). JSON Web Token (JWT) is an open standard (RFC 7519) that defines a compact and self-contained way for securely transmitting information between parties as a JSON object. This information can be verified and trusted because it is digitally signed.

In this exercise, our application is divided in two microservices. 
    
        1. AuthenticationService
        2. CategoryService

### Problem Statement

CategoryService is already implemented. Here you need to implement JWT authentication as per following:
     
     1. Implement Authentication Service with SQL server as backend.
     2. Authentication service should provie endpoints for Register and Login.
     2. Once the user is successfully registered with Autentication service, then on Login it should generate and return a JWT.
     3. Refactor CategoryService to integrate JWT authentication.

### Steps to be followed:

    Step 1: Clone the boilerplate in a specific folder on your local machine.
    Step 2: Go thru the readme.md file and implement the code for CategoryService and run the test cases.

### Project structure

The folders and files you see in this repositories, is how it is expected to be in projects, which are submitted for automated evaluation by Hobbes

    Project
	|
	├── AuthenticationService                   // This is the microservice for User Authentication
	├── CategoryService                         // This is the microservice of Category   
	├── .gitignore			                    // This file contains a list of file name that are supposed to be ignored by git 
	├── .hobbes   			                    // Hobbes specific config options, such as type of evaluation schema, type of tech stack etc., Have saved a default values for convenience
	

#### To use this as a boilerplate for your new project, you can follow these steps

1. Clone the base boilerplate in the folder **exercise-15** of your local machine
     
    `git clone https://gitlab-cts.stackroute.in/stack_aspnetbytelearning/exercise-15.git exercise-15`

2. Navigate to assignment-solution-step3 folder

    `cd exercise-15`

3. Remove its remote or original reference

     `git remote rm origin`

4. Create a new repo in gitlab named `assignment-solution-step3` as private repo

5. Add your new repository reference as remote

     `git remote add origin https://gitlab-cts.stackroute.in/{{yourUserName}}/exercise-15.git`

     **Note: {{yourUserName}} should be replaced by your userName from gitlab**

5. Check the status of your repo 
     
     `git status`

6. Use the following command to update the index using the current content found in the working tree, to prepare the content staged for the next commit.

     `git add .`
 
7. Commit and Push the project to git

     `git commit -a -m "Initial commit | or place your comments according to your need"`

     `git push -u origin master`

8. Check on the git repo online, if the files have been pushed

### Important instructions for Participants
> - We expect you to write the assignment on your own by following through the guidelines, learning plan, and the practice exercises
> - The code must not be plagirized, the mentors will randomly pick the submissions and may ask you to explain the solution
> - The code must be properly indented, code structure maintained as per the boilerplate and properly commented
> - Follow through the problem statement shared with you

### MENTORS TO BEGIN REVIEW YOUR WORK ONLY AFTER ->
> - You add the respective Mentor as a Reporter/Master into your Assignment Repository
> - You have checked your Assignment on the Automated Evaluation Tool - Hobbes (Check for necessary steps in your Boilerplate - README.md file. ) and got the required score - Check with your mentor about the Score you must achieve before it is accepted for Manual Submission.
> - Intimate your Mentor on Slack and/or Send an Email to learner.support@stackroute.in - with your Git URL - Once you done working and is ready for final submission.