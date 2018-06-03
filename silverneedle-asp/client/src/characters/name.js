import React, { Component } from 'react';
import { Button, Icon, Input } from 'semantic-ui-react';

class Name extends Component {
    constructor(props) {
        super(props);
        this.onRerollName = this.onRerollName.bind(this);
    }

    render() {
        const name = this.props.first_name + ' ' + this.props.last_name;
        return(
            <Input 
                label="Name" 
                placeholder="Name" 
                action={{ icon: 'undo', onClick: this.onRerollName}} 
                value={name} />
        );
    }

    async onRerollName() {
        //Connect to name api and request a new name....
        const response = await fetch("/characters/name/create?race=human");
        const json = await response.json();
        this.props.onNameChange(json.firstname, json.lastname);
    }
}

export default Name;