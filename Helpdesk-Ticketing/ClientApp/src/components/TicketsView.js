import React, { Component } from 'react';
import { setAccessToken, setUser, isLoggedIn } from './helpers';
import { Link, withRouter } from 'react-router-dom';


export class TicketsView extends Component {
    static displayName = TicketsView.name;

    constructor(props) {
        super(props);
        this.state = { ticketTypes: [], loading: true, isLoggedIn: true };
    }

    componentDidMount() {
        this.populateTicketData();
    }

    logout(event) {
        event.preventDefault()
        localStorage.removeItem('usertoken')
        this.props.history.push('/')
    }

    static renderTicketsTable(ticketTypes) {
        return (
            <table className='table table-striped' aria-labelledby="tabelLabel">
                <thead>
                    <tr>
                        <th>ID</th>
                        <th>NameTicket</th>
                        <th>Comments</th>
                    </tr>
                </thead>
                <tbody>
                    {ticketTypes.map(ticketType =>
                        <tr key={ticketType.ID}>
                            <td>{ticketType.ID}</td>
                            <td>{ticketType.NameTicket}</td>
                            <td>{ticketType.Comments}</td>
                        </tr>
                    )}
                </tbody>
            </table>
        );
    }

    render() {
        if (this.state.loggedIn) {
            window.location.replace("/");
            return true;
        }

        let contents = this.state.loading
            ? <p><em>Please choose a Help Ticket Below</em></p>
            : TicketsView.renderTicketsTable(this.state.ticketTypes);


        return (
            <div class="">
                <h1 id="tabelLabel" >Tickets Area</h1>
                <p>--------------------------------------------</p>
                <p>--------------------------------------------</p>
                <h1>Here are all the HelpDesk ticket options to select.</h1>
                <h2 id="tabelLabel" >TICKETS</h2>
                <p>This better work.</p>
                {contents}
                <div>
                   The user is <b>{isLoggedIn ? 'currently' : 'not'}</b> logged in.
                </div>
            </div>
        );
    }

    async populateTicketData() {
        const response = await fetch('Models/CartTickets');
        const data = await response.json();
        this.setState({ ticketTypes: data, loading: false });
    }
}




