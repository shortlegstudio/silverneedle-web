import { EventEmitter } from 'events';
import auth0 from 'auth0-js';
import { AUTH_CONFIG } from './auth-variables';
import history from '../history';

export default class Auth extends EventEmitter {
    auth0 = new auth0.WebAuth({
        domain: AUTH_CONFIG.domain,
        clientID: AUTH_CONFIG.clientID,
        redirectUri: AUTH_CONFIG.callbackUrl,
        audience: AUTH_CONFIG.apiUrl,
        responseType: 'token id_token',
        scope: 'openid profile'
    });

    userProfile;

    constructor() {
        super();
        this.login = this.login.bind(this);
        this.logout = this.logout.bind(this);
        this.handleAuthentication = this.handleAuthentication.bind(this);
        this.isAuthenticated = this.isAuthenticated.bind(this);
        this.getAccessToken = this.getAccessToken.bind(this);
        this.getProfile = this.getProfile.bind(this);
    }

    login() {
        this.auth0.authorize();
    }

    handleAuthentication() {
        this.auth0.parseHash((err, authResult) => {
            if(this.validateAuthResult(authResult)) {
                this.setSession(authResult);
                history.replace('/home');
            } else if(err) {
                history.replace('/home');
                console.log(err);
            }
        });
    }

    setSession(authResult) {
        if(this.validateAuthResult(authResult)) {
            let expiresAt = JSON.stringify(
                authResult.expiresIn * 1000 + new Date().getTime()
            );

            localStorage.setItem('access_token', authResult.accessToken);
            localStorage.setItem('id_token', authResult.idToken);
            localStorage.setItem('expires_at', expiresAt);
            history.replace('/home');
        }
    }

    validateAuthResult(authResult) {
        return (authResult && authResult.accessToken && authResult.idToken);
    }

    getAccessToken() {
        const access_token = localStorage.getItem('access_token');
        if(!access_token) {
            throw new Error('No access token found');
        }

        return access_token;
    }

    getProfile(cb) {
        let access_token = this.getAccessToken();
        this.auth0.client.userInfo(access_token, (err, profile) => {
            if(profile) {
                this.userProfile = profile;
                localStorage.username = profile.nickname;
            }
            if(cb)
                cb(err, profile);
        });
    }

    logout() {
        localStorage.removeItem('access_token');
        localStorage.removeItem('id_token');
        localStorage.removeItem('expires_at');
        this.userProfile = null;
        history.replace('/home');
    }

    isAuthenticated() {
        let expiresAt = JSON.parse(localStorage.getItem('expires_at'));
        return new Date().getTime() < expiresAt;
    }
}