# YouTube Uploader Application

## Description
The YouTube Uploader Application is a robust tool designed to automate the uploading of video files to YouTube. It utilizes Google's YouTube API and integrates various services to streamline operations such as video processing, authentication, and error handling.

## Features
- **Automated Video Upload**: Upload videos automatically to predefined YouTube playlists.
- **Error Handling**: Comprehensive error handling and logging to troubleshoot and ensure reliability.
- **Google Authentication**: Implements OAuth2.0 to securely authenticate with Google.
- **Flexible Video Selection**: Selects video files from a specified directory based on supported formats.

## Installation
To get started with the YouTube Uploader Application, clone this repository and install the required dependencies:

```bash
git clone https://github.com/vcszigmantas/youtubeUploader.git
cd youtubeUploader
dotnet restore
```
## Google Cloud Setup
To use the YouTube API, follow these steps to set up Google Cloud:

1. **Create a Google Cloud Project**:
   - Go to the [Google Cloud Console](https://console.cloud.google.com/).
   - Create a new project.

2. **Enable the YouTube Data API v3**:
   - Navigate to the 'API & Services' dashboard.
   - Click on 'Enable APIs and Services' and search for 'YouTube Data API v3'.
   - Enable it for your project.

3. **Configure OAuth Consent Screen**:
   - Under 'APIs & Services', go to 'OAuth consent screen'.
   - Set up the consent screen for external users. Fill in the application name, user support email, and developer contact information.

4. **Create Credentials**:
   - In the 'Credentials' tab, click 'Create Credentials' and choose 'OAuth client ID'.
   - Set the application type to 'Desktop app' and name it appropriately.
   - Download the 'client_secrets.json' file and place it in your project directory.

5. **Set up the File Data Store**:
   - Ensure your application can securely store and retrieve authentication tokens.

For more details, visit the [Google Developers Console](https://console.developers.google.com/).

## Usage
To run the application, execute the following command from the terminal within the project directory:

```bash
dotnet run
```
## Contribution
Contributions are welcome! If you have improvements or bug fixes, please fork the repository and submit a pull request. Follow these steps to contribute:

1. **Fork the repository**: Click on the 'Fork' button at the top right corner of this GitHub page.
2. **Clone your forked repository**: Clone your now forked repository to your machine.
3. **Create a new branch**: Navigate to your local repository and checkout a new branch with a descriptive name for your changes.
4. **Make your changes**: Implement your fixes, features, or improvements.
5. **Commit your changes**: Add your changes to your local repository and commit them with a clear, descriptive message.
6. **Push your changes to your fork**: Push your changes to your GitHub fork.
7. **Create a pull request**: Go back to the original repository and click on 'Pull Request'. Choose your branch and submit the pull request with a clear description of your changes.

## Testing
Before submitting a pull request, please make sure your changes do not break the existing functionality. Add any necessary tests to cover your changes and ensure all tests pass.

## License
This project is licensed under the MIT License.

## Contacts
For support or further information, please contact us via [zigmantas.rackauskas@gmail.com].

## FAQ
- **Q: What video formats are supported?**
  - A: The application supports .mp4, .mov, .avi, .wmv, .flv, .mkv, and .webm formats.
- **Q: How do I troubleshoot authentication errors?**
  - A: Ensure the 'client_secrets.json' is correctly placed and you have completed the OAuth consent screen setup.
- **Q: Where do I report a bug or suggest a feature?**
  - A: Please use the Issues section of this GitHub repository to report bugs or suggest features.

## Acknowledgements
This project uses the following open source software:
- [Entity Framework Core](https://github.com/dotnet/efcore) - For database operations.
- [Google API Client Libraries](https://github.com/googleapis/google-api-dotnet-client) - For interacting with Google services.

Thank you to all the contributors who spend time and effort making this project better!

