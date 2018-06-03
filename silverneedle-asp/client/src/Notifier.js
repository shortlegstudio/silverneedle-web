import React, { Component } from 'react';
import { Message } from 'semantic-ui-react';

let showNotifierHandler;

export default class Notifier extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            show: false,
            failure: false,
            title: '',
            message: ''
        }
    }

    componentDidMount() {
        showNotifierHandler = this.showNotifier;
    }
    render() {
        const message = this.state.message;
        const show = this.state.show;
        const failure = this.state.failure;
        if(!show)
            return null;

        return (
            <Message negative={failure} success={!failure}>
                <Message.Header>{this.state.title}</Message.Header>
                <p>{message}</p> 
            </Message>
        );
    }
    showNotifier = (message, failure, title) => {
        this.setState({
            show: true,
            message: message,
            title: title,
            failure: failure
        });
    }

}

export function showNotifier(message, failure=false, title='') {
    showNotifierHandler(message, failure, title);
}