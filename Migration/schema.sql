CREATE TABLE "User" (
  "Id" SERIAL PRIMARY KEY,
  "Name" text,
  "RoleId" int,
  "CreateDateTime" timestamp
);

CREATE TABLE "Role" (
  "Id" SERIAL PRIMARY KEY,
  "Title" text
);

CREATE TABLE "UserSecurity" (
  "UserId" int PRIMARY KEY,
  "Login" text,
  "Password" text,
  "Email" text
);

CREATE TABLE "UserQuiz" (
  "Id" SERIAL PRIMARY KEY,
  "UserId" int,
  "QuizId" int,
  "EmployeeId" int
);

CREATE TABLE "Quiz" (
  "Id" SERIAL PRIMARY KEY,
  "CreateDateTime" timestamp,
  "StatusId" int
);

CREATE TABLE "Question" (
  "Id" SERIAL PRIMARY KEY,
  "QuizId" int,
  "QuestionTypeId" int,
  "Text" text,
  "CreateDateTime" timestamp
);

CREATE TABLE "Answer" (
  "Id" SERIAL PRIMARY KEY,
  "QuestionId" int,
  "Text" text,
  "CreateDateTime" timestamp
);

CREATE TABLE "QuestionTemplate" (
  "Id" int PRIMARY KEY,
  "Text" text,
  "QuestionTypeId" int
);

CREATE TABLE "AnswerTamplate" (
  "Id" int PRIMARY KEY,
  "QuestionTamplateId" int,
  "Text" text
);

CREATE TABLE "UserAnswer" (
  "Id" SERIAL PRIMARY KEY,
  "EmployeeId" int,
  "QuizId" int,
  "QuestionId" int,
  "AnswerId" int,
  "CreateDateTime" timestamp
);

CREATE TABLE "QuestionType" (
  "Id" SERIAL PRIMARY KEY,
  "Title" text
);

CREATE TABLE "Employee" (
  "Id" SERIAL PRIMARY KEY,
  "Name" text,
  "Soname" text,
  "DateOfBirth" timestamp,
  "Gender" text,
  "PositionId" int,
  "RoleId" int
);

CREATE TABLE "Position" (
  "Id" SERIAL PRIMARY KEY,
  "Title" text
);

CREATE TABLE "UserEmployee" (
  "Id" SERIAL PRIMARY KEY,
  "UserId" int,
  "EmployeeId" int
);

CREATE TABLE "Status" (
  "Id" SERIAL PRIMARY KEY,
  "Title" text
);

ALTER TABLE "User" ADD FOREIGN KEY ("RoleId") REFERENCES "Role" ("Id");

ALTER TABLE "UserSecurity" ADD FOREIGN KEY ("UserId") REFERENCES "User" ("Id");

ALTER TABLE "UserQuiz" ADD FOREIGN KEY ("UserId") REFERENCES "User" ("Id");

ALTER TABLE "UserQuiz" ADD FOREIGN KEY ("QuizId") REFERENCES "Quiz" ("Id");

ALTER TABLE "UserQuiz" ADD FOREIGN KEY ("EmployeeId") REFERENCES "Employee" ("Id");

ALTER TABLE "Quiz" ADD FOREIGN KEY ("StatusId") REFERENCES "Status" ("Id");

ALTER TABLE "Question" ADD FOREIGN KEY ("QuizId") REFERENCES "Quiz" ("Id");

ALTER TABLE "Question" ADD FOREIGN KEY ("QuestionTypeId") REFERENCES "QuestionType" ("Id");

ALTER TABLE "Answer" ADD FOREIGN KEY ("QuestionId") REFERENCES "Question" ("Id");

ALTER TABLE "QuestionTemplate" ADD FOREIGN KEY ("QuestionTypeId") REFERENCES "QuestionType" ("Id");

ALTER TABLE "AnswerTamplate" ADD FOREIGN KEY ("QuestionTamplateId") REFERENCES "QuestionTemplate" ("Id");

ALTER TABLE "UserAnswer" ADD FOREIGN KEY ("EmployeeId") REFERENCES "Employee" ("Id");

ALTER TABLE "UserAnswer" ADD FOREIGN KEY ("QuizId") REFERENCES "Quiz" ("Id");

ALTER TABLE "UserAnswer" ADD FOREIGN KEY ("QuestionId") REFERENCES "Question" ("Id");

ALTER TABLE "UserAnswer" ADD FOREIGN KEY ("AnswerId") REFERENCES "Answer" ("Id");

ALTER TABLE "Employee" ADD FOREIGN KEY ("PositionId") REFERENCES "Position" ("Id");

ALTER TABLE "Employee" ADD FOREIGN KEY ("RoleId") REFERENCES "Role" ("Id");

ALTER TABLE "UserEmployee" ADD FOREIGN KEY ("UserId") REFERENCES "User" ("Id");

ALTER TABLE "UserEmployee" ADD FOREIGN KEY ("EmployeeId") REFERENCES "Employee" ("Id");
