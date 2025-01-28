# magic-squares

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

Now I have my dev environment sorted, so next on the agenda is to set up the backend project.