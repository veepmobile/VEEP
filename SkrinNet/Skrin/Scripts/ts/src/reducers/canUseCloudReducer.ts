import {RECEIVE_CAN_USE_CLOUD} from '../models/Constants'
import {IActionRecieveCanUseCloud} from '../actions/AccessCloudActionCreators'


export const can_use_cloud=(state:boolean=false,action:IActionRecieveCanUseCloud)=>{
    switch (action.type) {
        case RECEIVE_CAN_USE_CLOUD:
            return state=action.can_use_cloud;
        default:
            return state;
    }
}

