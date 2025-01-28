# magic-squares

## Installing dependencies

1. Get VS Code and Docker installed.
2. Add the "Dev Containers" extension installed in VS Code.
3. In VS Code, open the command palette and type "Dev Containers". Select the alternative "Open Folder in Container...", and select the project root folder.
4. Once the container has started:
   - Install the backend dependencies by `cd`:ing into MagicSquaresApi in the integrated terminal and run `dotnet restore`.
   - Install the frontend dependencies by `cd`:ing into TBD in the integrated terminal and run `npm install`.

## Running it

### Backend

Using VS Code's integrated terminal, run `dotnet run` from the MagicSquaresApi to run the backend.
You can test the API using Scalar [>HERE<](http://localhost:5011/scalar/v1).

## Requirements

- Frontend
  - Written in React.js
  - A CSS framework is not necessary, but if one is used, it should preferably be TailwindCSS.
  - For every click on "Lägg till ruta", a randomly colored square is added according to the behavior pattern:
    - The required behavior can be seen in [this PDF](https://github.com/Wizardworks-AB/programmeringsuppgift/blob/master/Wizardworks%20-%20programmeringsuppgift.pdf).
    - The required behavior can be experienced on [this website](https://www.wizardworks.se/Squares).
- Backend
  - Written in C#/.NET
  - Implements an API
    - On every button click, the color and position of the new square is sent via the API to be stored to a file in JSON format.
    - When the web page reloads, the current state is read from the file via the API.

## Design choices

### Where should the layout logic live?
The first design choice I will have to make is where the layout logic should live. Although the layout logic follows a pattern, it is somewhat quirky.

If a client came to me with a quirky layout requirement, I would assume that it could change to something completely different (still quirky) in the future. Therefore, I choose to implement the layout logic completely in the frontend.

This will make the frontend and backend less coupled. If the layout logic requirements change, I only have to make changes to the frontend. The backend and its API becomes a lot simpler, in fact the "add square" operation becomes an "append to a list" operation. This means that the backend does not need to keep track of positions, as they are implied by the position in the list. The APIs request body only needs to include a color. The "restore state" operation will just be a simple list of color codes, as the frontend will handle the parsing and rendering of the list.

## Dev Diary

### 2025-01-28
I've run through a .NET + React tutorial on Microsoft Learn. Takeaways:
1. I've probably found the `dotnet` versions of the Maven/Gradle stuff I'm used to.
2. The tutorial was using devcontainers. I've never used it before, but it was nice, so I added it to this project.

I've started the backend project. Added Scalar, which seems nice. I think I have all the business logic sorted for the backend. I wrote that in a
separate class, and added some unit test for it with an in-memory version of the file writer.

Next time, I'll have to implement the "real" file writer and stich up the API. Then we're off to frontend land for a bit.