import http from 'k6/http';
3	
export default function () {
  http.get('http://localhost:3000/joke');
}
