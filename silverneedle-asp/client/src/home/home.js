import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import {
    Header,
    Segment
} from 'semantic-ui-react';
import Hero from './hero';

class Home extends Component {
    componentWillMount() {
        const { isAuthenticated, getProfile } = this.props.auth;
    }
    login() {
      this.props.auth.login();
    }
    render() {
      const { isAuthenticated } = this.props.auth;
      return (
          <div>
            <Hero />
            {this.props.children}
        </div>
      );
    }
  }
  
  export default Home;