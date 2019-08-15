# PowerChain Blockchain Solution&nbsp;&nbsp;&nbsp;[![Build Status](https://travis-ci.com/passlickdev/powerchain.svg?token=pYSRUkhcJkH5kPyosazr&branch=master)](https://travis-ci.com/passlickdev/powerchain)

Simple fully-integrated blockchain solution, optimized for versatility, performance and security. Cross-platform (works on Windows, Linux and macOS), built on .NET Core and written in C#. Developed by Passlick Development.  
*This software and its source code are licensed under **GNU/GPLv3** (see LICENSE for more details).*  

<br>
  
## Functions

This software is currently in an early alpha state. At this state, the PowerChain blockchain solution only handles _local_ blockchains and is not ready to maintain a functioning blockchain network. You can generate, load, save and validate a local blockchain, represented by an object of the class `Blockchain`, which consists of blocks represented by objects of the class `Block`. It's also possible to add data to and get data from a _local_ blockchain. The local blockchain is stored in a JSON file called `blockchain.json`. Currently, the solution lacks an implementation of a proof-of-work algorithm.  

__Feel free to help this software becoming a great blockchain solution by adding code and submitting push requests!__ 

<br>

### Current functions:
- Initialize a new blockchain, including the _Genesis block_
- Load a local blockchain from a file (`blockchain.json`)
- Save a blockchain to a JSON file
- Validate the integrity of a blockchain
- Add data (`str`) to a blockchain
- Get data from a blockchain (by hash)

### Functions in-development:
- Proof-of-work algorithm for adding data to a blockchain
- Ability to maintain a strong blockchain network with multiple peers and/or miners (p2p/server-based)
- Add your own functions! :smile:
  
<br>

## Documentation

### Arguments
You can pass arguments either as a parameter when starting the application (`/[ARGUMENT]`) or within the in-application CLI (`[ARGUMENT]`). Every argument can be either passed in upper or lower cases.

<br>

| __Argument (parameter)__ | __Argument (CLI)__ |                                                                                     __Definition__                                                                                     |
|:--------------------:|:--------------:|----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
|         `/run`         |       `run`      | Currently no function.                                                                                                                                                              |
|         `/init`        |      `init`      | Initializes a new local blockchain (including the genesis block).                                                                                                                                                 |
|     `/load [PATH]`     |   `load [PATH]`  | Loads a local blockchain file. Uses default path when no path is given.<br><br>Default path: `%Documents%\Passlick Development\data\blockchain.json`<br>Example: `load "C:\Users\User\Desktop"`                                        |
|     `/save [PATH]`     |   `save [PATH]`  | Saves the current blockchain. Uses default path when no path is given.<br><br>Default path: `%Documents%\Passlick Development\data\blockchain.json`<br>Example: `save "C:\Users\User\Desktop\blockchain.json"` |
|       `/validate`      |    `validate`    | Validates the loaded blockchain. A valid blockchain is required for following arguments to work: `save`, `add`, `get`                                                                    |
|      `/add [DATA]`     |   `add [DATA]`   | Adds a new block with data (`str`) to the loaded blockchain.<br>Example: `add "{timestamp=2019-08-15T18:43:10.4557493Z;hash=167cd7}"`                                   |
|      `/get [HASH]`     |   `get [HASH]`   | Returns data from the loaded blockchain by SHA256 hash.<br>Example: `get 6eb10c0bcfef93aa85fefad9f8078be4cfce52935413820ef376d[...]`                                             |
|        `/about`        |      `about`     | Returns information about the software.                                                                                                                                            |
|         `/help`        |      `help`      | Opens the PowerChain help.                                                                                                                                                         |
|         `/exit`        |      `exit`      | Exits the application.                                                                                                                                                             |

<br>
<br>

### Run the application
Download either the ZIP with a self-contained assembly (marked with the respective OS, i.e. '`_win-x86`') or the ZIP with a framework-dependent assembly (marked with '`_dotnetcore`'). Follow the steps below to run the application.

<br>

|    __OS__   |                        __Run self-contained assembly__                       |                                __Run framework-dependent assembly__                                |
|:-------:|------------------------------------------------------------------------|----------------------------------------------------------------------------------------------|
| Windows | 1. Navigate to folder with assembly<br> 2. Run '`PowerChain.exe`'                                                  | 1. Run cmd / PowerShell<br>2. Navigate to folder with assembly<br>3. Shell: `Shell> dotnet PowerChain.dll` |
|  Linux  | 1. Run bash<br>2. Navigate to folder with assembly<br>3. Bash: `$ ./PowerChain`     | 1. Run bash<br>2. Navigate to folder with assembly<br>3. Bash: `$ dotnet PowerChain.dll`                  |
|  macOS  | 1. Run Terminal<br>2. Navigate to folder with assembly<br>3. Terminal: `$ ./PowerChain` | 1. Run Terminal<br>2. Navigate to folder with assembly<br>3. Terminal: `$ dotnet PowerChain.dll`              |

<br>
<br>

### Build the application

To build the application yourself, download or clone the whole repository to your local machine, open your OS's respective shell, navigate to the repository and run following commands:
- __Self-contained assembly__: `dotnet publish .\powerchain.csproj --configuration Release --framework netcoreapp2.1 --self-contained --runtime [RID]`<br>(For RID, see https://docs.microsoft.com/de-de/dotnet/core/rid-catalog)
- __Portable, runtime-dependent assembly__: `dotnet publish .\powerchain.csproj --configuration Release --framework netcoreapp2.1`

> ⚠ **Note**: To build the application yourself, you need to have the .NET Core SDK 2.1 installed!

<br>

## System requirements
- OS: Microsoft Windows 7+, Linux (ARM/x64) or macOS 10.12+
- Other: .NET Core runtime (.NET Core 2.1) installed for non-self-contained assemblies
  
<br>

## Submit Issue
  
Use the [*Issue*](https://github.com/passlickdev/powerchain/issues) function in this GitHub repository to submit any issues!

<br>

## Download binaries

**[Download Binary from *Releases*](https://github.com/passlickdev/powerchain/releases)**  
For older versions, see *Releases* in the GitHub repository