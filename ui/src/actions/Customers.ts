import { User, ID } from '../types'

const customerCollection = 'http://localhost:5000/api/customers/'

export const getCustomers = () => {
  return fetch(customerCollection, {
    method: 'GET',
    headers: new Headers({ Accept: 'application/json' }),
  }).then((res) => res.json())
}

export const setCustomer = (user: User) => {
  return fetch(customerCollection, {
    method: 'POST',
    headers: new Headers({ 'Content-Type': 'application/json' }),
    body: JSON.stringify({ ...user }),
  }).then((res) => res.json())
}

export const deleteCustomer = (id: ID) => {
  return fetch(customerCollection + id, {
    method: 'DELETE',
    headers: new Headers({ Accept: 'application/json' }),
  }).then((res) => res.status)
}

export const updateCustomer = (id: ID, user: User) => {
  return fetch(customerCollection + id, {
    method: 'PUT',
    headers: new Headers({ 'Content-Type': 'application/json' }),
    body: JSON.stringify({ ...user }),
  }).then((res) => res.json())
}
