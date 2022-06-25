# Advanced Programming 2 - ex3 - server side
### Authors:

**Roei Cohen, Eithan Rospsha**

### Usage:
notice that the android project is in the following repo - https://github.com/roei-stack/Advanced_Programming2_P3
<br><br>
If you just want to use the server then clone the repository, we recommend to choose option `open with visual studio` and start `BorisKnowsAllApi` with VS or dotnet.
<br><br>
If you want to check the server and the android with the web client use the following steps (recommended):<br><br>
0. Make sure you have download the following: `asp.net6` `node.js`<br>
1. clone the repository, (choose option `open with visual studio`)<br>
2. start `BorisKnowsAllApi` and `BorisWeb` with VS or `dotnet`
3. Go to `react-development/src/data/data.js` and make sure `API_URL` IS EQUAL TO `BorisKnowsAllApi` base address<br>
4. navigate to `react-development`<br>
5. open the terminal, and type `npm install` then `npm start`<br>

#### How is works:

We used `javascript`, `Bootstrap`, `React`, `asp.net` <br>
Used packetges: `react`, `react-don` `react-router-dom` `signalR`.<br>
database structure:<br>
Class `User: username, password, nickname, contacts[]`<br>
Class `Contact: id, name, messages[]`<br>
Class `Message: id, content, sent, created`<br>
Class `Rate: rating, feedback, name`<br>
