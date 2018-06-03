import React from 'react';
import { Redirect, Route, BrowserRouter } from 'react-router-dom';
import App from './App';
import Auth from './auth/auth';
import history from './history';

import Home from './home/home';
import Profile from './profile/profile';
import Callback from './callback/callback';
import CharacterList from './characters/character-list';
import CharacterGenerator from './characters/character-generator';
import Notifier from './Notifier';
import {
    Container 
} from 'semantic-ui-react';

const auth  = new Auth();

const handleAuthentication = (nextState, replace) => {
    if(/access_token|id_token|error/.test(nextState.location.hash)) {
        auth.handleAuthentication();
    }
}

export const makeMainRoutes = () => {
    return (
        <BrowserRouter history={history} component={App}>
            <div>
                <Route path="/" render={(props) => <App auth={auth} {...props} />} />
                <Container inverted> 
                    <Notifier />
                    <Route path="/home" render={(props) => <Home auth={auth} {...props} />} />
                    <Route path="/profile" render={(props) => (
                        !auth.isAuthenticated() ? (
                            <Redirect to="/home/" />
                        ) : (
                            <Profile auth={auth} {...props} />
                        )
                    )} />
                    <Route path="/callback" render={(props) => {
                        handleAuthentication(props);
                        return <Callback {...props} />
                    }} />
                    <Route exact path="/characters/" render={(props) => <CharacterList auth={auth} {...props} />} />
                    <Route path="/characters/generator" render={(props) => <CharacterGenerator auth={auth} {...props} />} />
                </Container>
            </div>
        </BrowserRouter>
    );
}