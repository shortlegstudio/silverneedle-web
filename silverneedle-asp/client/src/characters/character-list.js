import React, { Component } from 'react';
import {
    Button, 
    Container,
    Icon,
    Menu,
    Segment,
    Table
} from 'semantic-ui-react';

function CharacterListItem(props) {
    return ( 
        <Table.Row>
            <Table.Cell>{props.item.first_name}</Table.Cell>
            <Table.Cell>{props.item.last_name}</Table.Cell>
        </Table.Row>
    );
}

class CharacterList extends Component {
    constructor(props) {
        super(props);
        this.state = {list: []};
    }

    componentDidMount() {
        this.loadCharacterList();
    }

    render() {
        const { isAuthenticated } = this.props.auth;
        var list = this.state.list.map((character) => 
            <CharacterListItem item={character} />
        );
        return (
            <Segment>
                <Menu attached='top' compact>
                    <Menu.Menu position='right'>
                        <Menu.Item><Button color='blue' href='/characters/generator'><Icon name='add' />New</Button></Menu.Item>
                    </Menu.Menu>
                </Menu>
                <Table>
                    <Table.Header>
                        <Table.Row>
                            <Table.Cell>First Name</Table.Cell>
                            <Table.Cell>Last Name</Table.Cell>
                            <Table.Cell>Actions</Table.Cell>
                        </Table.Row>
                    </Table.Header>
                    <Table.Body>
                        {list}
                    </Table.Body>
                </Table>
            </Segment>
        );
    }

    async loadCharacterList() {
        const response = await fetch("/characters/", {
            headers: {
                "Authorization" : `Bearer ${this.props.auth.getAccessToken()}` 
            }
        });
        const list = await response.json();
        this.setState({list: list});
    }
}

export default CharacterList;