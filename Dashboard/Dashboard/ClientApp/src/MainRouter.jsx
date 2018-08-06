import React, { Component } from 'react';
import { Router, Route } from 'react-router';
import App from './App';
import HomePage from './pages/HomePage';
import SettingsPage from './pages/SettingsPage';
import PractitionerPanel from './components/practitioners/PractitionerPanel';
import PractitionerDashboard from './components/practitioners/PractitionerDashboard';
export default class MainRouter extends Component {
  constructor() {
    super();
    this.state = {
      navOpenState: {
        isOpen: true,
        width: 304,
      }
    }
  }

  onNavResize = (navOpenState) => {
    this.setState({
      navOpenState,
    });
  }

  render() {
    return (
      <App
          onNavResize={this.onNavResize}
          navOpenState={this.state.navOpenState}
          
          >
          <Route exact path="/" component={HomePage} />
          <Route path="/settings" component={SettingsPage} />
          <Route exact path="/practitioners" component={PractitionerPanel}/>
          <Route path="/practitioners/:id" render={(props) => <PractitionerDashboard {...props} />} />
        </App>
    );
  }
}