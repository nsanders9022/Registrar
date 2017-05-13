# Registrar

#### By _**Nicole Sanders, Kory Skarbek**_

## Description

This program adds students to a database and signs up students for a course.

## Specs

1. Check to see if students table is empty
* **Input:**
* **Output:** true

2. Program recognizes two courses instances as equal if they have the same name
* **Input:** Kory, Kory
* **Output:** true

3. Program will save entries into the student table
* **Input:** Kory
* **Output:** Kory

4. Program will return true if a student has a unique id and has been saved to an object
* **Input:** Nicole
* **Output:** true

5. Program will return true if the student item has been found in the database
* **Input:** John
* **Output:** true

6. Check to see if courses table is empty
* **Input:**
* **Output:** true

7. Program recognizes two courses instances as equal if they have the same name
* **Input:** Coding, Coding
* **Output:** true

8. Program will save entries into the courses table
* **Input:** Math
* **Output:** Math

9. Program will return true if a course has a unique id and has been saved to an object
* **Input:** Business
* **Output:** true

10. Program will return true if the course item has been found in the database
* **Input:** P.E.
* **Output:** true

11. Check to see if students_courses table is empty
* **Input:**
* **Output:** true

12. Program will add a student to a course
* **Input:** Kory
* **Output:** Coding

13. Program will return all the students in a course
* **Input:** Coding
* **Output:** Kory, Nicole, John

14. Program will return all the courses a student has
* **Input:** Nicole
* **Output:** Coding, Math, P.E.


## Setup/Installation Requirements

* Requires DNU, DNX, and Mono
* Clone to local machine
* Use command "dnu restore" in command prompt/shell
* Use command "dnx kestrel" to start server
* Navigate to http://localhost:5004 in web browser of choice

## Known Bugs

There are no known bugs. Please contact us if any are discovered.

## Support and contact details

Contact Nicole Sanders at nsanders9022@gmail.com for any questions, comments, or concerns.

## Technologies Used

* C#
* Nancy Framework
* Razor
* SQL

### License

*This software is licensed under the MIT license*

Copyright (c) 2017 **_Nicole Sanders, Kory Skarbek_**

