import * as React from "react";
import { connect } from 'react-redux';

import {IIssuerState} from "../../models/IIssuerState";
import {ModalActionCreators} from '../../actions/ModalActionCreators'
import ConfirmModalView from './ConfirmModalView'

const mapStateToProps=(state:IIssuerState)=>({
    isShowing:state.modals.isShowing,
    body:state.modals.body,
    action:state.modals.action
})

const mapDispatchToProps=(dispatch:any)=>({
    hideModal:()=>dispatch(ModalActionCreators.hideModal()),
    accept:(action:any)=>{
        dispatch(ModalActionCreators.hideModal());
        dispatch(action);
    }
})

export default connect(mapStateToProps,mapDispatchToProps)(ConfirmModalView)