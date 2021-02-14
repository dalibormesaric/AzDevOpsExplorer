import React from 'react';
import ReactDOM from 'react-dom';
import './index.css';
import { ProjectsList } from './features/projects/ProjectList';
import { ReleaseList } from './features/releases/ReleaseList';
import { Release } from './features/releases/Release';
import { BuildList } from './features/builds/BuildList';
import reportWebVitals from './reportWebVitals';
import { Switch, Route, BrowserRouter as Router } from "react-router-dom";

ReactDOM.render(
  <React.StrictMode>
    <div className="container mx-auto px-4">
      <Router>
        <Switch>
          <Route exact path="/">
            <ProjectsList />
          </Route>
          <Route exact path="/releases/:project">
            <ReleaseList />
          </Route>
          <Route exact path="/releases/:project/:id">
            <Release />
          </Route>
          <Route exact path="/builds/:project">
            <BuildList />
          </Route>
        </Switch>
      </Router>
    </div>
  </React.StrictMode>,
  document.getElementById('root')
);

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();
