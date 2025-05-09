-- Insert Projects

INSERT INTO Projects (ProjectName, StartDate, EndDate, Description) VALUES

('Predictive Planning', '2024-01-01', '2024-06-30', 'AI-driven planning and forecasting system'),

('Project Retrospective', '2024-02-01', '2024-07-31', 'Analysis and reporting system for completed projects');



-- Insert Employees (including Managers)

INSERT INTO People (FirstName, LastName, Email, PhoneNumber, Street, City, State, ZipCode, HireDate, Experience, Salary, ManagerID) VALUES

('Alice', 'Johnson', 'alice@example.com', '1234567890', '123 Main St', 'New York', 'NY', '10001', '2020-05-10', 5, 90000, NULL), -- Manager of Project 1

('Bob', 'Smith', 'bob@example.com', '9876543210', '456 Elm St', 'San Francisco', 'CA', '94107', '2018-03-15', 7, 95000, NULL), -- Manager of Project 2

('Charlie', 'Brown', 'charlie@example.com', '1112223333', '789 Oak St', 'Chicago', 'IL', '60601', '2021-07-22', 3, 70000, 1),

('David', 'Miller', 'david@example.com', '4445556666', '321 Pine St', 'Houston', 'TX', '77002', '2019-09-30', 4, 75000, 1),

('Grace', 'Lee', 'grace@example.com', '5556667777', '369 Birch St', 'Denver', 'CO', '80202', '2019-11-05', 6, 82000, 1),

('Hannah', 'Taylor', 'hannah@example.com', '9990001111', '852 Spruce St', 'Miami', 'FL', '33101', '2023-02-17', 1, 60000, 1),

('Ian', 'White', 'ian@example.com', '3334445555', '147 Walnut St', 'Austin', 'TX', '73301', '2020-06-21', 3, 71000, 1),

('Leo', 'Scott', 'leo@example.com', '1113335555', '753 Redwood St', 'Chicago', 'IL', '60602', '2020-10-30', 3, 72000, 2),

('Mia', 'Adams', 'mia@example.com', '4446668888', '951 Hickory St', 'San Diego', 'CA', '92101', '2019-07-19', 4, 74000, 2),

('Olivia', 'Green', 'olivia@example.com', '5557779999', '654 Fir St', 'Seattle', 'WA', '98102', '2022-05-05', 2, 66000, 2),

('Paul', 'Baker', 'paul@example.com', '3335557777', '357 Cypress St', 'Boston', 'MA', '02109', '2021-03-07', 3, 70000, 2),

('Quinn', 'Harris', 'quinn@example.com', '9991113333', '258 Aspen St', 'Denver', 'CO', '80203', '2019-09-12', 4, 76000, 2)



-- Insert Roles (excluding Business Analyst)

INSERT INTO Roles (RoleName) VALUES

('Project Manager'),

('Software Engineer'),

('QA Engineer'),

('Data Analyst'),

('UI/UX Designer'),

('DevOps Engineer');



-- Assign Roles to Employees (Updated)

INSERT INTO EmployeeRoles (EmployeeID, RoleID) VALUES

(1, 1),  -- Alice Johnson (Manager of Project 1)

(2, 1),  -- Bob Smith (Manager of Project 2)

(3, 2),  -- Charlie Brown (Software Engineer)

(4, 3),  -- David Miller (QA Engineer)

(5, 5),  -- Grace Lee (UI/UX Designer)

(6, 2),  -- Hannah Taylor (Software Engineer)

(7, 4),  -- Ian White (Data Analyst)

(8, 2),  -- Leo Scott (Software Engineer)

(9, 2),  -- Mia Adams (Software Engineer)

(10, 6), -- Olivia Green (DevOps Engineer)

(11, 3), -- Paul Baker (QA Engineer) -- Updated Role

(12, 4); -- Quinn Harris (Data Analyst)



INSERT INTO Skills (SkillName, SkillLevel, Description) VALUES

('Leadership', 'Expert', 'Ability to lead teams and projects effectively'),

('Project Management', 'Expert', 'Planning, executing, and monitoring projects'),

('Communication', 'Advanced', 'Effective team and stakeholder communication'),

('Python', 'Advanced', 'Programming in Python for development'),

('Java', 'Advanced', 'Programming in Java for application development'),

('C++', 'Intermediate', 'Programming in C++ for performance-critical applications'),

('Problem-Solving', 'Advanced', 'Analytical and logical problem-solving skills'),

('Automation Testing', 'Intermediate', 'Test automation using frameworks'),

('Selenium', 'Intermediate', 'Automation testing using Selenium'),

('Bug Tracking', 'Intermediate', 'Tracking and managing software defects'),

('SQL', 'Advanced', 'Database querying and data management'),

('Data Visualization', 'Intermediate', 'Creating reports and dashboards'),

('Excel', 'Intermediate', 'Using Excel for data processing and reporting'),

('Figma', 'Intermediate', 'Prototyping and UI design using Figma'),

('User Research', 'Intermediate', 'Understanding user behavior and needs'),

('CI/CD', 'Advanced', 'Continuous integration and deployment'),

('Docker', 'Advanced', 'Containerization using Docker'),

('Cloud Computing', 'Advanced', 'Working with cloud platforms like AWS, Azure');





INSERT INTO EmployeeSkills (EmployeeID, SkillID, AcquiredDate) VALUES

-- Managers (Leadership, Project Management, Communication)

(1, 1, '2020-06-01'), (1, 2, '2020-06-01'), (1, 3, '2020-06-01'),  -- Alice Johnson (Manager of Project 1)

(2, 1, '2018-04-10'), (2, 2, '2018-04-10'), (2, 3, '2018-04-10'),  -- Bob Smith (Manager of Project 2)



-- Project 1 Team

(3, 4, '2021-08-15'), (3, 5, '2021-08-15'), (3, 7, '2021-08-15'),  -- Charlie Brown (Software Engineer) - Python, Java, Problem-Solving

(4, 8, '2019-10-05'), (4, 9, '2019-10-05'), -- David Miller (QA Engineer) - Automation Testing, Selenium

(5, 14, '2019-12-01'), (5, 15, '2019-12-01'),  -- Grace Lee (UI/UX Designer) - Figma, User Research

(6, 6, '2023-03-10'), (6, 7, '2023-03-10'),  -- Hannah Taylor (Software Engineer) - C++, Problem-Solving

(7, 11, '2020-07-20'), (7, 12, '2020-07-20'), -- Ian White (Data Analyst) - SQL, Data Visualization



-- Project 2 Team

(8, 4, '2020-11-05'), (8, 5, '2020-11-05'), (8, 7, '2020-11-05'),  -- Leo Scott (Software Engineer) - Python, Java, Problem-Solving

(9, 4, '2019-08-21'), (9, 7, '2019-08-21'),  -- Mia Adams (Software Engineer) - Python, Java, Problem-Solving

(10, 16, '2022-06-30'), (10, 17, '2022-06-30'), (10, 18, '2022-06-30'), -- Olivia Green (DevOps Engineer) - CI/CD, Docker, Cloud Computing

(11, 8, '2021-04-12'), (11, 9, '2021-04-12'), (11, 10, '2021-04-12'), -- Paul Baker (QA Engineer) - Automation Testing, Selenium, Bug Tracking

(12, 11, '2019-09-15'), (12, 12, '2019-09-15'), (12, 13, '2019-09-15'); -- Quinn Harris (Data Analyst) - SQL, Data Visualization, Excel



-- Insert Contributions

INSERT INTO Contributions (ContributionDate, Description) VALUES

('2024-03-01', 'Designed and implemented core Python modules for AI-based prediction'),

('2024-03-02', 'Created automation testing framework for AI algorithms'),

('2024-03-03', 'Designed UI prototypes and conducted user research'),

('2024-03-04', 'Improved performance of C++ algorithms for better efficiency'),

('2024-03-05', 'Developed interactive dashboards for predictive analytics'),

('2024-03-06', 'Implemented backend services using Python and Java'),

('2024-03-07', 'Developed API integrations and wrote unit tests'),

('2024-03-08', 'Set up automated deployment using CI/CD pipelines'),

('2024-03-09', 'Executed automation testing and bug tracking processes'),

('2024-03-10', 'Generated SQL-based reports and analyzed project data');



-- Insert Evaluations

INSERT INTO Evaluations (Score, Comments, EvaluationDate) VALUES

(90.5, 'Excellent Python development and problem-solving skills.', '2024-04-01'),

(75.0, 'Good automation testing skills, needs improvement in bug tracking.', '2024-04-02'),

(82.0, 'Effective UI/UX design and user research implementation.', '2024-04-03'),

(91.0, 'Strong C++ development with performance optimizations.', '2024-04-04'),

(85.0, 'Clear and insightful data visualization for analytics.', '2024-04-05'),

(92.0, 'Excellent backend development and API handling.', '2024-04-06'),

(74.0, 'Good Java development but needs more focus on testing.', '2024-04-07'),

(95.0, 'Strong DevOps skills with efficient CI/CD pipeline.', '2024-04-08'),

(80.0, 'Accurate and efficient automation testing.', '2024-04-09'),

(83.5, 'Comprehensive data analysis and SQL querying.', '2024-04-10');



-- Insert Performances

-- Insert Performances

INSERT INTO Performances (ManagerID, EmployeeRoleID, ProjectID, ContributionID, EvaluationID, HoursWorked,Year) VALUES

(1, 3, 1, 1, 1, 160 ,2024-05-20),  -- Python Development

(1, 4, 1, 2, 2, 140,2024-05-23),  -- Automation Testing

(1, 5, 1, 3, 3, 150,2024-05-23),  -- UI/UX Prototyping

(1, 6, 1, 4, 4, 170,2024-05-20),  -- C++ Optimization

(1, 7, 1, 5, 5, 145,2024-05-25),  -- Data Visualization

(2, 8, 2, 6, 6, 175,2024-05-20),  -- Backend Development

(2, 9, 2, 7, 7, 160,2024-05-21),  -- API Development

(2, 10, 2, 8, 8, 180,2024-05-22), -- CI/CD Implementation

(2, 11, 2, 9, 9, 155,2024-05-22), -- Automated Testing

(2, 12, 2, 10, 10, 165,2024-05-25); -- SQL Report Generation

