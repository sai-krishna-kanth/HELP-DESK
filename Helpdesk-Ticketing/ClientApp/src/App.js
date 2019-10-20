import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { Counter } from './components/Counter';
import { LoginView } from './components/LoginView';
import { RegistrationView } from './components/RegistrationView';
import { TicketsView } from './components/TicketsView';

import './custom.css'


export default class App extends Component {
  static displayName = App.name;

  render () {
    return (
      <Layout>
        <Route exact path='/' component={Home} />
            <Route path='/counter' component={Counter} />

            <Route path='/login-view' component={LoginView} />

            <Route path='/registration-view' component={RegistrationView} />

            <Route path='/tickets-view' component={TicketsView} />
      </Layout>
    );
  }
}
