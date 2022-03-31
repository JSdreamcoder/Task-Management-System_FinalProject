# Task-Management-System_FinalProject
## Instructions
## Create a Task Management System to help a company track the performance in their projects, a  “Project” contains many “Tasks”, there are multiple roles in this system, a Project Manager can  create projects and tasks, and can assign tasks to Developers.

- Create the necessary classes using Entity Framework Code First and Authentication.

- Create a User Manager class that has functions to manage users and roles (Get all roles for a user, assign roles to users, check if a user in a role)...etc

- Create a ProjectHelper class that contains functions to add, delete, update projects, along with any other helper functions. Those functions can only be accessed by project managers.

- Create a TaskHelper class that contains functions to add, delete, update tasks, and assign a task to a developer, those functions can only be accessed by project managers.

- Developers can view all their tasks (but can’t view other developers’ tasks), also a developer can update a task to change the completed percentage of the task, or mark it totally completed, when the task is marked completed, the developer can leave a comment to describe any notes or hints.

- A project manager can view a project and all the tasks for that project, ordered by the completion percentage (the completed ones appear first), he can also choose to hide the complete tasks.

- We need to add a new field (using migrations) to determine the priority of the task or a project (enum), now the previous page will have the option to order the tasks based on priority.

- A Project manager will have a dashboard page which shows all the projects with their related tasks and assigned developers, the projects with high priorities appear first.

- Add a new migration to add the field “Deadline” to tasks and projects. A developer will get a notification when the task is only one day to pass the deadline. The Developers should be able to see the number of notifications in their homepage, they can click on this number to go to a page where they can see all the notifications in detail.

- The project manager can see a list of the tasks that are not finished yet and passed their deadline.

- The project manager will get a notification if a project passed a deadline with any unfinished tasks.

- Developers can leave an urgent Note to a task to mention a bug or a problem preventing them from completing the task, in this case a notification is sent to the Project Manager.

- A project manager gets a notification whenever a task or a project is completed.

- Add a new property to the notifications to determine if it is new or opened (unread).

- Add a link called “Notifications” to the project manager dashboard which will take the manager to see all his notifications, this link also shows the number of current unopened notifications.

- We need to support the “Budget” functionality in our Projects. When you create a project, you need to provide the assigned budget to this project. Also when you create a new User in the system, you need to define the daily salary of this User (e.g. developer number 4 gets paid 200$ a day, Manager Number 1 gets 1000$ for managing each project).

- Create a page for Project Managers where they can see the Projects that exceeded their Budgets.

- When a Project is done, the Project Manager should be able to see what the total cost of this Project is.
