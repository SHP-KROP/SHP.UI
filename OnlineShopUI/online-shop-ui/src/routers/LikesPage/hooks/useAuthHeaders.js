export default function useToken() {
  let token = localStorage.getItem('token');

  return { headers: { "Authorization": `Bearer ${token}`}};
}
