
import {IAddress,IAddressField} from '../models/IAddress'
import { GenerateGUID, GetDate } from '../lib/Utilites'
import {DeepCopy} from '../lib/Utilites'

export default class IssuerUserAddressesFakeAPI
{
    private static fake_adresses:IAddress[]=[
        {
            id:GenerateGUID(),
            name:'Иванов Иван Иванович',
            phone:'345-23-23',
            email:'test@test.ru',
            note:'Должен мне 100 рублей',
            user_id: 889,
            issuer_id: '3DC84DD11D61CC51C32567400032199E',
            update_date: '20.12.2016',
            extrafields:[
                {
                    key:1,
                    value:'skrin.ru'
                }
            ]
        }, 
        {
            id:GenerateGUID(),
            name:'Петя',
            phone:'999-99-99',
            email:'',
            note:'',
            user_id: 889,
            issuer_id: '3DC84DD11D61CC51C32567400032199E',
            update_date: '20.12.2016',
            extrafields:[]
        }
    ]

    public static Get(user_id: number, issuer_id: string): Promise<IAddress[]> {
        let addresses = this.fake_adresses.filter(a => {
            return a.user_id == user_id && a.issuer_id == issuer_id
        });
        return new Promise<IAddress[]>((resolve, reject) => {
            resolve(DeepCopy<IAddress[]>(addresses));
        })
    }

    public static Update(address:IAddress):Promise<IAddress>{

        let old_address=this.fake_adresses.filter(p=>p.id==address.id);


        if(old_address.length!==1){
            this.fake_adresses.push(address);            
        }else{
            old_address[0]=address;
        }

        return new Promise<IAddress>((resolve, reject) => {
                setTimeout(function(){
                    resolve(DeepCopy<IAddress>(address));
            },500);
        });
    }

    public static Delete(id:string): Promise<IAddress>{
        return new Promise<IAddress>((resolve, reject)=>{
            let addresses=this.fake_adresses.filter(a=>a.id==id);
            if(addresses.length!==1){
                reject(new Error("address not found"));
            }
            let a_index=this.fake_adresses.indexOf(addresses[0]);
            this.fake_adresses.splice(a_index,1);
            resolve(DeepCopy<IAddress>(addresses[0]));
        });
    }
}