# SyncGuardian
SyncGuardian: Your data's trusted guardian, connecting mobile and desktop for seamless backup, access, and peace of mind.

## Table of Contents

1. [Features](#features)
2. [Getting Started](#getting-started) (Pending)
   - [Installation](#installation) (Pending)
   - [Usage](#usage) (Pending)
3. [Contributing](#contributing)
4. [Testing](#testing)
5. [License](#license)
6. [Acknowledgements](#acknowledgements) (Pending)
7. [Contact](#contact) (Pending)
---
### Features

Discover the robust set of features that make SyncGuardian a powerful tool for managing your files effortlessly. From seamless synchronization to intuitive user controls, SyncGuardian offers:

- **Real-time File Synchronization**: Keep your files updated in real-time across devices and platforms.

- **Selective Folder Monitoring**: Choose specific folders to monitor and sync, providing flexibility and efficiency.

- **Cross-Platform Compatibility**: Utilize SyncGuardian on Windows, Android, and upcoming support for Linux, macOS, iOS, and iPadOS.

- **Intuitive User Interface**: An easy-to-use interface ensuring smooth navigation and a seamless user experience.

- **Secure and Private**: Your data remains protected, and privacy is a priority with SyncGuardian.

- **Customizable Settings**: Tailor settings according to your preferences, optimizing the synchronization process.

---
### Contributing

We welcome contributions from the community to enhance SyncGuardian. If you'd like to contribute, please follow these guidelines:

1. **Fork the Repository**: Fork this repository to your own GitHub account.
2. **Create a Branch**: Create a branch with a descriptive name to work on the proposed changes (`git checkout -b <BranchName>`).
3. **Make Changes**: Make the desired changes, add features, or fix bugs.
4. **Run Tests**: Ensure that your changes do not break existing functionality and include any necessary tests.
5. **Commit Changes**: Commit your changes with a clear and concise commit message (`git commit -m 'Add new feature'`).
6. **Push Changes**: Push your changes to the branch (`git push origin <Branchname>`).
7. **Open a Pull Request**: Create a pull request from your branch to the `main` branch of this repository.

For proper details please check [Code Guidelines](CONTRIBUTING.md) and ensure that your contributions adhere to our standards.

Thank you for contributing to SyncGuardian! Your support helps make this project better and more useful for everyone.
---
### Testing

We maintain a comprehensive set of tests to ensure the stability and functionality of SyncGuardian. We have separate test projects for the WPF application and the mobile applications.

Testing Project is named "SyncGuardianTests"

Over there you will find 2 folders, "WPF" and "Android" respectively. When ever you create a new functionality or service, you will need to create test cases for those. 

SyncGuardianTests is a "NUnit Test Framework" project. Please follow the instructions to create a test case

1. Open "SyncGuardianTests" project in Visual Studio. 
2. Create a new test class in the appropriate folder (WPF or Android).

```
[TestFixture]
public class YourComponentTests
{
    // Your test cases will go here
}
```
3. Write test methods:
```
[Test]
public void YourTestMethod()
{
    // Arrange: Prepare necessary data or set up the test environment
    var someComponent = new SomeComponent();

    // Act: Perform the action you want to test
    var result = someComponent.DoSomething();

    // Assert: Check the result or behavior
    Assert.AreEqual(expectedValue, result);
}
```
4. Arrange, Act, and Assert (AAA):
   - Arrange: Set up the necessary preconditions and inputs for the test.
   - Act: Perform the action or method that you want to test.
   - Assert: Verify the expected results or behavior.

5. Run specific test case
6. Once every new test cases are created, Run All test cases.

#### Contributing and Testing
We strongly encourage contributors to create and run tests relevant to the changes they are making. Before submitting a pull request, please make sure to run all the relevant tests to verify that the changes do not introduce any regressions or issues. This helps maintain the stability and quality of SyncGuardian.

Thank you for your contributions and for ensuring the integrity of the project through rigorous testing!



---
### License
SyncGuardian is licensed under [Apache License 2.0](LICENSE). Click on the following link to read the full license details: [Read License](LICENSE)