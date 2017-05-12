import * as React from "react";
import { connect } from 'react-redux';

import {IIssuerState} from "../../models/IIssuerState";
import {AccessCloudActionCreators} from '../../actions/AccessCloudActionCreators'
import CanUseCloudCheckView from './CanUseCloudCheckView'


const mapStateToProps=(state:IIssuerState)=>({
    isShowing:state.can_use_cloud_modals.isShowing,
    action:state.can_use_cloud_modals.action
})

const mapDispatchToProps=(dispatch:any)=>({
    checkCloudUsing:()=>dispatch(AccessCloudActionCreators.CanUseCloud()),
    hideModal:()=>dispatch(AccessCloudActionCreators.hideModal()),
    accept:(action:any)=>dispatch(AccessCloudActionCreators.ConfirmCanUseCloud(action))
})

export default connect(mapStateToProps,mapDispatchToProps)(CanUseCloudCheckView)