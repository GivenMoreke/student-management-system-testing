using System;
using System.IO;
using System.Collections.Generic;
using Xunit;
using PRG282_Project.DataLayer;
using PRG282_Project.BussinessLogicLayer;

namespace SWT262_Tests
{
    // =====================================================================
    // TASK 2: WHITE-BOX TESTING
    // Techniques: Statement Coverage, Branch Coverage, Condition Coverage
    // =====================================================================

    /// <summary>
    /// Suite 1: Statement Coverage
    /// Ensures every executable statement in BLL and FileHandler is executed.
    /// </summary>
    public class StatementCoverageTests : IDisposable
    {
        private readonly string _testLoginFile = "TestLoginDetails.txt";
        private readonly BLL _bll;

        public StatementCoverageTests()
        {
            _bll = new BLL();
            // Create a test login file with a known user
            File.WriteAllText(_testLoginFile, "testuser,testpass");
        }

        [Fact]
        public void LogInValidation_ValidCredentials_ReturnsTrue()
        {
            // Arrange
            string username = "admin";
            string password = "1234";

            // Act
            bool result = _bll.LogInValidation(username, password);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void LogInValidation_EmptyUsername_ReturnsFalse()
        {
            // Arrange
            string username = "";
            string password = "1234";

            // Act
            bool result = _bll.LogInValidation(username, password);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void StudentDataValidation_ValidData_ReturnsTrue()
        {
            // Arrange
            string studentNumber = "50001";
            string nameSurname = "Given Moreke";
            string picturePath = "image.png";
            string dob = "01-01-2000";
            string gender = "Female";
            string phone = "0711234567";
            string address = "10 Test Street, Pretoria";
            List<string> courses = new List<string> { "PRG282" };

            // Act
            bool result = _bll.studentDataValidation(studentNumber, nameSurname, picturePath, dob, gender, phone, address, courses);

            // Assert
            Assert.True(result);
        }

        public void Dispose()
        {
            if (File.Exists(_testLoginFile)) File.Delete(_testLoginFile);
        }
    }

    /// <summary>
    /// Suite 2: Branch Coverage
    /// Ensures every true/false branch in decision statements is executed.
    /// </summary>
    public class BranchCoverageTests
    {
        private readonly BLL _bll;

        public BranchCoverageTests()
        {
            _bll = new BLL();
        }

        [Fact]
        public void NewUserValidation_AllFieldsFilled_ReturnsTrue()
        {
            // Arrange
            string username = "newuser";
            string password = "pass123";
            string confirmPassword = "pass123";

            // Act
            bool result = _bll.newUserValidation(username, password, confirmPassword);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void NewUserValidation_EmptyConfirmPassword_ReturnsFalse()
        {
            // Arrange
            string username = "newuser";
            string password = "pass123";
            string confirmPassword = "";

            // Act
            bool result = _bll.newUserValidation(username, password, confirmPassword);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void StudentDataValidation_NonNumericStudentNumber_ReturnsFalse()
        {
            // Arrange — student number contains letters, should fail numeric check branch
            string studentNumber = "ABC12";
            string nameSurname = "Given Moreke";
            string picturePath = "image.png";
            string dob = "01-01-2000";
            string gender = "Female";
            string phone = "0711234567";
            string address = "10 Test Street, Pretoria";
            List<string> courses = new List<string> { "PRG282" };

            // Act
            bool result = _bll.studentDataValidation(studentNumber, nameSurname, picturePath, dob, gender, phone, address, courses);

            // Assert
            Assert.False(result);
        }
    }

    /// <summary>
    /// Suite 3: Condition Coverage
    /// Ensures every individual boolean condition evaluates to both true and false.
    /// </summary>
    public class ConditionCoverageTests
    {
        private readonly BLL _bll;

        public ConditionCoverageTests()
        {
            _bll = new BLL();
        }

        [Fact]
        public void CourseDataValidation_AllFieldsProvided_ReturnsTrue()
        {
            // Arrange — all conditions true
            string courseID = "PRG282";
            string courseName = "Programming 282";
            string link = "https://youtube.com/example";
            string description = "Object Relational Programming";

            // Act
            bool result = _bll.courseDataValidation(courseID, courseName, link, description);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void CourseDataValidation_MissingCourseID_ReturnsFalse()
        {
            // Arrange — courseID condition is false
            string courseID = "";
            string courseName = "Programming 282";
            string link = "https://youtube.com/example";
            string description = "Object Relational Programming";

            // Act
            bool result = _bll.courseDataValidation(courseID, courseName, link, description);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void SearchValidation_EmptySearchTerm_ReturnsFalse()
        {
            // Arrange — condition evaluates to false
            string number = "";

            // Act
            bool result = _bll.searchValidation(number);

            // Assert
            Assert.False(result);
        }


        // =====================================================================
        // TASK 3: BLACK-BOX TESTING
        // Techniques: BVA, Equivalence Partitioning, Decision Table Testing
        // =====================================================================


        /// <summary>
        /// Suite 1: Boundary Value Analysis (BVA)
        /// Tests input at exact boundaries of valid ranges.
        /// </summary>
        public class BoundaryValueAnalysisTests : IDisposable
        {
            private readonly string _testLoginFile = "LoginDetails.txt";
            private readonly FileHandler _fileHandler;

            public BoundaryValueAnalysisTests()
            {
                _fileHandler = new FileHandler();
                File.WriteAllText(_testLoginFile, "admin,1234");
            }

            [Fact]
            public void AddUser_UsernameWithOneCharacter_ReturnsSuccess()
            {
                // Arrange — minimum boundary: single character username
                File.WriteAllText(_testLoginFile, "admin,1234");
                string username = "a";
                string password = "pass1";
                string confirmPass = "pass1";

                // Act
                int result = _fileHandler.AddUser(username, password, confirmPass);

                // Assert
                Assert.Equal(0, result);
            }

            [Fact]
            public void CheckUser_CorrectCredentials_ReturnsTrue()
            {
                // Arrange — at valid boundary: exact matching credentials
                string username = "admin";
                string password = "1234";

                // Act
                bool result = _fileHandler.CheckUser(username, password);

                // Assert
                Assert.True(result);
            }

            [Fact]
            public void CheckUser_WrongPasswordByOneCharacter_ReturnsFalse()
            {
                // Arrange — just outside valid boundary: password off by one character
                string username = "admin";
                string password = "12345";

                // Act
                bool result = _fileHandler.CheckUser(username, password);

                // Assert
                Assert.False(result);
            }

            public void Dispose()
            {
                if (File.Exists(_testLoginFile)) File.Delete(_testLoginFile);
            }
        }

        /// <summary>
        /// Suite 2: Equivalence Partitioning
        /// Divides inputs into valid and invalid partitions and tests a representative from each.
        /// </summary>
        public class EquivalencePartitioningTests : IDisposable
        {
            private readonly string _testLoginFile = "LoginDetails.txt";
            private readonly FileHandler _fileHandler;

            public EquivalencePartitioningTests()
            {
                _fileHandler = new FileHandler();
                File.WriteAllText(_testLoginFile, "admin,1234");
            }

            [Fact]
            public void AddUser_ValidPartition_ReturnsZero()
            {
                // Arrange — valid partition: no spaces, no commas, passwords match, user does not exist
                File.WriteAllText(_testLoginFile, "admin,1234");
                string username = "validuser";
                string password = "validpass";
                string confirmPass = "validpass";

                // Act
                int result = _fileHandler.AddUser(username, password, confirmPass);

                // Assert
                Assert.Equal(0, result);
            }

            [Fact]
            public void AddUser_InvalidPartition_UsernameHasSpace_ReturnsOne()
            {
                // Arrange — invalid partition: username contains a space
                string username = "invalid user";
                string password = "pass123";
                string confirmPass = "pass123";

                // Act
                int result = _fileHandler.AddUser(username, password, confirmPass);

                // Assert
                Assert.Equal(1, result);
            }

            [Fact]
            public void AddUser_InvalidPartition_PasswordMismatch_ReturnsThree()
            {
                // Arrange — invalid partition: passwords do not match
                string username = "newuser2";
                string password = "password1";
                string confirmPass = "password2";

                // Act
                int result = _fileHandler.AddUser(username, password, confirmPass);

                // Assert
                Assert.Equal(3, result);
            }

            public void Dispose()
            {
                if (File.Exists(_testLoginFile)) File.Delete(_testLoginFile);
            }
        }

        /// <summary>
        /// Suite 3: Decision Table Testing
        /// Tests combinations of input conditions and expected system behaviour.
        ///
        /// Conditions for AddUser:
        /// C1: Username contains space or comma
        /// C2: Password contains space or comma
        /// C3: Passwords match
        /// C4: Username already exists
        ///
        /// Expected outcomes: errorNum 0 (success), 1, 2, 3, or 4
        /// </summary>
        public class DecisionTableTests : IDisposable
        {
            private readonly string _testLoginFile = "LoginDetails.txt";
            private readonly FileHandler _fileHandler;

            public DecisionTableTests()
            {
                _fileHandler = new FileHandler();
                File.WriteAllText(_testLoginFile, "existinguser,pass123");
            }

            [Fact]
            public void AddUser_C1True_InvalidUsername_ReturnsOne()
            {
                // C1=true: username has comma — should return error 1
                string username = "user,name";
                string password = "pass123";
                string confirmPass = "pass123";

                int result = _fileHandler.AddUser(username, password, confirmPass);

                Assert.Equal(1, result);
            }

            [Fact]
            public void AddUser_C2True_InvalidPassword_ReturnsTwo()
            {
                // C1=false, C2=true: password has space — should return error 2
                string username = "validuser";
                string password = "pass 123";
                string confirmPass = "pass 123";

                int result = _fileHandler.AddUser(username, password, confirmPass);

                Assert.Equal(2, result);
            }

            [Fact]
            public void AddUser_C4True_DuplicateUsername_ReturnsFour()
            {
                // C1=false, C2=false, C3=true, C4=true: user already exists
                string username = "existinguser";
                string password = "newpass";
                string confirmPass = "newpass";

                int result = _fileHandler.AddUser(username, password, confirmPass);

                Assert.Equal(4, result);
            }

            public void Dispose()
            {
                if (File.Exists(_testLoginFile)) File.Delete(_testLoginFile);
            }
        }


        // =====================================================================
        // TASK 4: AUTOMATED TESTING WITH xUnit
        // Testing FileHandler methods: AddPicture, CheckUser, AddUser
        // =====================================================================

        /// <summary>
        /// Automated xUnit tests for the FileHandler class.
        /// Uses [Fact], Arrange, Act, Assert pattern.
        /// </summary>
        public class FileHandlerAutomatedTests : IDisposable
        {
            private readonly string _testLoginFile = "LoginDetails.txt";
            private readonly string _testImagesDir = "Images";
            private readonly FileHandler _fileHandler;

            public FileHandlerAutomatedTests()
            {
                _fileHandler = new FileHandler();
                File.WriteAllText(_testLoginFile, "admin,1234");
                if (!Directory.Exists(_testImagesDir))
                    Directory.CreateDirectory(_testImagesDir);
            }

            // --- AddPicture Tests ---

            [Fact]
            public void AddPicture_ValidFilePath_ReturnsFileName()
            {
                // Arrange
                string testImagePath = "test_image.png";
                File.WriteAllBytes(testImagePath, new byte[] { 0x89, 0x50, 0x4E, 0x47 }); // PNG header bytes

                // Act
                string result = _fileHandler.AddPicture(testImagePath);

                // Assert
                Assert.Equal("test_image.png", result);

                // Cleanup
                if (File.Exists(testImagePath)) File.Delete(testImagePath);
                if (File.Exists($@"Images\test_image.png")) File.Delete($@"Images\test_image.png");
            }

            [Fact]
            public void AddPicture_DuplicateFile_ReturnsUniqueFileName()
            {
                // Arrange — copy an image that already exists in Images folder
                string testImagePath = "duplicate_image.png";
                File.WriteAllBytes(testImagePath, new byte[] { 0x89, 0x50, 0x4E, 0x47 });
                File.WriteAllBytes($@"Images\duplicate_image.png", new byte[] { 0x89, 0x50, 0x4E, 0x47 });

                // Act
                string result = _fileHandler.AddPicture(testImagePath);

                // Assert — should return a modified unique name, not the original
                Assert.NotEqual("duplicate_image.png", result);

                // Cleanup
                if (File.Exists(testImagePath)) File.Delete(testImagePath);
                if (File.Exists($@"Images\duplicate_image.png")) File.Delete($@"Images\duplicate_image.png");
                if (File.Exists($@"Images\{result}")) File.Delete($@"Images\{result}");
            }

            // --- CheckUser Tests ---

            [Fact]
            public void CheckUser_ValidCredentials_ReturnsTrue()
            {
                // Arrange
                string username = "admin";
                string password = "1234";

                // Act
                bool result = _fileHandler.CheckUser(username, password);

                // Assert
                Assert.True(result);
            }

            [Fact]
            public void CheckUser_InvalidPassword_ReturnsFalse()
            {
                // Arrange
                string username = "admin";
                string password = "wrongpassword";

                // Act
                bool result = _fileHandler.CheckUser(username, password);

                // Assert
                Assert.False(result);
            }

            [Fact]
            public void CheckUser_NonExistentUser_ReturnsFalse()
            {
                // Arrange
                string username = "ghostuser";
                string password = "1234";

                // Act
                bool result = _fileHandler.CheckUser(username, password);

                // Assert
                Assert.False(result);
            }

            // --- AddUser Tests ---

            [Fact]
            public void AddUser_ValidNewUser_ReturnsZero()
            {
                // Arrange
                string username = "newuser";
                string password = "securepass";
                string confirmPass = "securepass";

                // Act
                int result = _fileHandler.AddUser(username, password, confirmPass);

                // Assert
                Assert.Equal(0, result);
            }

            [Fact]
            public void AddUser_PasswordsDoNotMatch_ReturnsThree()
            {
                // Arrange
                string username = "anotheruser";
                string password = "password1";
                string confirmPass = "password2";

                // Act
                int result = _fileHandler.AddUser(username, password, confirmPass);

                // Assert
                Assert.Equal(3, result);
            }

            [Fact]
            public void AddUser_UsernameContainsComma_ReturnsOne()
            {
                // Arrange
                string username = "bad,user";
                string password = "pass123";
                string confirmPass = "pass123";

                // Act
                int result = _fileHandler.AddUser(username, password, confirmPass);

                // Assert
                Assert.Equal(1, result);
            }

            public void Dispose()
            {
                if (File.Exists(_testLoginFile)) File.Delete(_testLoginFile);
                if (Directory.Exists(_testImagesDir)) Directory.Delete(_testImagesDir, true);
            }
        }
    }
}
