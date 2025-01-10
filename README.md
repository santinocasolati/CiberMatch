# CiberMatch

## How to Open the Project in Unity

1. Clone or download the repository to your local machine.
2. Open Unity Hub.
3. Click on the "Add" button in Unity Hub.
4. Navigate to the folder where you cloned/downloaded the repository and select the project folder.
5. The project will now appear in Unity Hub. Click on it to open the project in Unity.

## How to Run the Windows Build

1. Navigate to the [Releases](https://github.com/santinocasolati/CiberMatch/releases) section of the repository.
2. Download the latest Windows build.
3. Extract the contents of the file to a folder on your computer.
4. Inside the extracted folder, double-click on the `.exe` file to run the Windows build.

## How to Run the WebGL Build

1. Navigate to the [Releases](https://github.com/santinocasolati/CiberMatch/releases) section of the repository.
2. Download the latest Web build.
3. Extract the contents of the file to a folder on your computer.

To run the WebGL build in a browser, you need to serve it using a local web server.

### Option 1: Using Python's Built-in HTTP Server

1. Open a terminal or command prompt.
2. Navigate to the folder where the WebGL build is located (the folder containing `index.html`).
3. Run the following command:
   
   For Python 3.x:
   ```bash
   python -m http.server 8888
   ```
   
4. Open your browser and go to http://localhost:8888 to access the WebGL build.

### Option 2: Using Visual Studio Code (VS Code)

1. Install the "Live Server" extension from the VS Code marketplace.
2. Open the folder containing your WebGL build in VS Code.
3. Right-click on `index.html` and select **Open with Live Server**.
4. The WebGL build will open in your default browser.

### Option 3: In a restricted Itch.io page

1. Go to this restricted [Itch.io](https://sanntt.itch.io/cibermatch) page
2. Use the following password
   ```bash
   smartfense-santino
   ```
3. Click on the Run Game button and the game will load
