import React, { Component } from 'react';
import './App.css';
import { Button, Menu } from 'semantic-ui-react';

class App extends Component {
  goto(route) {
    this.props.history.replace(`/${route}`);
  }

  login() {
    this.props.auth.login();
  }

  logout() {
    this.props.auth.logout();
  }

  render() {
    const { isAuthenticated } = this.props.auth;
    return (
      <div>
        <Menu>
          <Menu.Item header href='/home/'>Silver Needle</Menu.Item>
          <Menu.Item href='/characters/'>Characters</Menu.Item>
            {
                !isAuthenticated() && (
                <Menu.Menu position='right'>
                  <Menu.Item ><Button primary onClick={this.login.bind(this)}>Login</Button></Menu.Item>
                </Menu.Menu>
                )
            }
            {
                isAuthenticated() && (
                <Menu.Menu position='right'>
                  <Menu.Item href='/profile/'>My Account</Menu.Item>
                  <Menu.Item ><Button secondary onClick={this.logout.bind(this)}>Logout</Button></Menu.Item>
                </Menu.Menu>
                )
            }
        </Menu>
      </div>
    );
  }
}

export default App;
