import {RECEIVE_ADDRESSES,RECEIVE_UPDATE_ADDRESS,RECEIVE_DELETE_ADDRESS} from '../models/Constants'
import {IAddress} from '../models/IAddress'
import {IAction} from '../models/Common'
import {IActionReceiveUserAddresses,IActionReceiveUserAddress} from '../actions/IssuerAddressActionCreators'
import {DeepCopy} from '../lib/Utilites'

export const user_addresses=(state:IAddress[]=[],action:IAction)=>{
    let address_index:number;
    let new_adress:IAddress;
    switch (action.type) {
        case RECEIVE_ADDRESSES:
            return DeepCopy<IActionReceiveUserAddresses>(action).addresses;
        case RECEIVE_DELETE_ADDRESS:
            new_adress=DeepCopy<IActionReceiveUserAddress>(action).address;
            address_index=state.indexOf(state.filter(p=>p.id===new_adress.id)[0]);
            state.splice(address_index,1);
            return[...state];
        case RECEIVE_UPDATE_ADDRESS:
            new_adress=DeepCopy<IActionReceiveUserAddress>(action).address;
            address_index=state.indexOf(state.filter(p=>p.id===new_adress.id)[0]);
            if(address_index==-1){
                 state.push(DeepCopy<IActionReceiveUserAddress>(action).address);
            }else{
                state[address_index]=new_adress;
            }            
            return [...state];
        default:
            return state;
    }
}