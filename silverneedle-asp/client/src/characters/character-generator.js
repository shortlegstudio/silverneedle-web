import React, { Component } from 'react';
import Name from './name';
import { 
    Button,
    Segment
} from 'semantic-ui-react';
import history from '../history';
import {showNotifier} from '../Notifier'

class CharacterGenerator extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            first_name: 'first',
            last_name: 'last'
        };

        this.onNameChange = this.onNameChange.bind(this);
        this.save = this.save.bind(this);
    }

    onNameChange(first, last) {
        this.setState({first_name: first, last_name: last});
    }

    render() {
        const authenticated = this.props.auth.isAuthenticated();
        const saveButton = authenticated ? (
            <Button primary onClick={this.save}>Save</Button>
        ) : ( <Button primary onClick={this.save} disabled>Save</Button>);
    
        return (
            <Segment>
                <h1>Character Generator</h1>
                <Name first_name={this.state.first_name} last_name={this.state.last_name} onNameChange={this.onNameChange} />
                <div>
                {saveButton}
                </div>
            </Segment>
        );
    }

    async save() {
        const { getAccessToken } = this.props.auth;
        var character = this.state;

        const response = await fetch("/characters/create", {
            method: 'post',
            body: JSON.stringify(character),
            headers: {
                "Content-Type": "application/json",
                "Authorization" : `Bearer ${getAccessToken()}`
            }
        });

        if(response.status === 200) {
            history.replace("/characters");
        } else {
            const msg = await response.json();
            showNotifier(msg.error, true, 'Saving Character');
        }
    }
}

export default CharacterGenerator;