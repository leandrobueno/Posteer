import React from "react";
import { Container } from "react-bootstrap";
import { Route, Switch } from "react-router";
import "./App.css";
import Home from "./components/layout/Home";
import PrivateRoute from "./components/PrivateRoute";
import Register from "./components/account/Register";
import Login from "./components/account/Login";
import ConfirmEmail from "./components/account/ConfirmEmail";
import List from "./components/todo/list";
import { ItemsProvider } from "./data/ItemsContext";

function App() {
  return (
    <>
      <Route exact path="/" component={Home} />
      <Route
        path={"/(.+)"}
        render={() => (
          <>
            <Container>
              <Switch>
                <Route path="/login" component={Login} />
                <Route path="/register" component={Register} />
                <Route path="/confirm" component={ConfirmEmail} />
                <ItemsProvider>
                  <PrivateRoute exact path="/tasks/list" component={List} />
                </ItemsProvider>
              </Switch>
            </Container>
          </>
        )}
      />
    </>
  );
}

export default App;
