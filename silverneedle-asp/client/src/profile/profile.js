import React, { Component } from 'react';
// Import Bootstrap components from react-bootstrap

class Profile extends Component {
  // componentWillMount() is invoked immediately before mounting occurs and we are setting the profile state to the value gotten from getprofile() which is called from the Auth service in Auth.js.
  componentWillMount() {
    this.setState({ profile: {} });
    const { userProfile, getProfile } = this.props.auth;

    // Check if there's a user profile, if there's none use the getProfile method from Auth. js to get a a profile and set it to the profile state.
    if (!userProfile) {
      getProfile((err, profile) => {
        this.setState({ profile });
      });
    } else {
      this.setState({ profile: userProfile });
    }
  }
  render() {
    // Using destructuring assignment to set the constant profile to the state
    const { profile } = this.state;
    return (
        <div>
          <h1>{profile.name}</h1>
            <img src={profile.picture} alt="profile" />
            <div>
              <h3>{profile.nickname}</h3>
            </div>
            <pre>{JSON.stringify(profile, null, 2)}</pre>
        </div>
    );
  }
}

export default Profile;