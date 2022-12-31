import jwt_decode from 'jwt-decode';
export function decodeJwt(token) {
            const decodedJwt = jwt_decode(token)
            this.$emit('currUser',decodedJwt["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"])

}