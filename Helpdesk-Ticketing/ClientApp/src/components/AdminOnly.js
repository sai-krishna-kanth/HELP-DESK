import React, { Component } from 'react';

export class AdminOnly extends Component {
    static displayName = AdminOnly.name;

    constructor(props) {
        super(props);
        this.state = { tickets: [{ NameTicket: "NameTicket", Comments: "Comments"}]};
        //this.state = { email: "", password: "" };
    }

    static renderUsers(username) {

        return (
            <table className='table table-striped' aria-labelledby="tabelLabel">
                <thead>
                    <tr>
                        <th>UserLogin</th>
                        <th>ID</th>
                        <th>Ticket-Name:</th>
                        <th>Comments</th>
                    </tr>
                </thead>
                <tbody>
                    {username.map(login =>
                        <tr key={login.id}>
                            <td>{login.id}</td>
                            <td>{login.name}</td>
                            <td>{login.comments}</td>
                        </tr>
                    )}
                </tbody>
            </table>
        );
    }



    render() {
        let contents = this.state.loading ? <p><em>Loading Users....</em></p> : LoginView.renderUsers(this.state.login);
        let { logginIn } = this.props;
        let { username, password, submitted } = this.state;
        return (
            <div>
                <h1>Admin to see all tickets</h1>
                <div className="col-4">
                    <div className="card product-card">
                        <div className="card-body">
                            <h4 className="card-title">Some Tickets</h4>
                            <p className="card-text">Ticket description</p>
                            {contents}
                        </div>
                    </div>
                </div>
            </div>
        );
    }
    async UserData() {
        //that is probably not right route...
        const response = await fetch('https://localhost:44361/Models/Accountusers');
        const data = await response.json();
        this.setState({ login: data, loading: true });
    }
}
