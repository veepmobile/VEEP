import * as React from "react";
import * as ReactDOM from "react-dom";
import { Provider } from 'react-redux';
import { createStore, applyMiddleware } from 'redux';

import thunk from 'redux-thunk';
import reducers from './reducers/rootReducer';
import {logger} from './lib/logger';
import {IIssuerState} from "./models/IIssuerState";


import IssuerUserFiles from './components/IssuerUserFiles/IssuerUserFilesComponent'
import IssuerUserNotes from './components/IssuerUserNotes/IssuerUserNotesComponent'
import IssuerUserAddress from './components/IssuerUserAddress/IssuerUserAddressComponent'
import ConfirmModal from './components/ConfirmModal/ConfirmModalComponent'
import CanUseCloudCheck from './components/CanUseCloudCheck/CanUseCloudCheckComponent'


var issuer_id=document.getElementById("cloud_container").getAttribute("data-id");

const InitState={
    issuer_id:issuer_id
}as IIssuerState

const IssuerStore=createStore(
    reducers,
    InitState,
    applyMiddleware(/*logger,*/thunk)
);


ReactDOM.render(
    <Provider store={IssuerStore}>
        <div>
            <CanUseCloudCheck/>
            <ConfirmModal/>
            <IssuerUserAddress/>
            <IssuerUserNotes />
            <IssuerUserFiles />
        </div>
    </Provider>,
    document.getElementById("cloud_container")
);