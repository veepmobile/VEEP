import * as React from "react";
import { connect } from 'react-redux';


import IssuerUserAddress  from "./IssuerUserAddressView";
import {IIssuerState} from "../../models/IIssuerState";
import {IssuerAddressActionCreators} from '../../actions/IssuerAddressActionCreators'
import {IAddress} from '../../models/IAddress'
import {ModalActionCreators} from '../../actions/ModalActionCreators'
import {AccessCloudActionCreators} from '../../actions/AccessCloudActionCreators'

const mapStateToProps=(state:IIssuerState)=>(
    {
        addresses:state.addresses,
        issuer_id:state.issuer_id,
        can_use_cloud:state.can_use_cloud
    }
);

const mapDispatchToProps=(dispatch:any)=>(
    {
        getAddresses:(issuer_id: string )=>(dispatch(IssuerAddressActionCreators.GetAddresses(issuer_id))),
        saveAddress:(address:IAddress,can_use_cloud:boolean)=>{
            if(can_use_cloud){
                    dispatch(IssuerAddressActionCreators.UpdateAddress(address));
                }else{
                    dispatch(AccessCloudActionCreators.showModal(IssuerAddressActionCreators.UpdateAddress(address)));
                } 
        },
        deleteAddress:(address_id:string,address_name:string)=>{
            dispatch(ModalActionCreators.showConfirmModal("Вы действительно хотите удалить контакт: "+address_name +"?",IssuerAddressActionCreators.DeleteAddress(address_id))) 
        }
    }
);

export default connect(mapStateToProps,mapDispatchToProps)(IssuerUserAddress)