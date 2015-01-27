using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace assets._Oh_My_Frog.XMLParser
{
    class MKeyValue {
        /*public string getString(string what, string default_value) {

        }


        std::string MKeyValue::getString(const std::string &what, const std::string default_value) const {
	const_iterator it = find (what);
	if (it == end())
		return default_value;
	return decodeFromUTF8 ( it->second.c_str(), strlen ( it->second.c_str() ) );
}*/
    }
}


/*------------------------------------------------------------------
| 
\------------------------------------------------------------------*/
/*std::string decodeFromUTF8(const char *data, size_t nMax) {    
	const unsigned char *szSource = (const unsigned char *) data;

	std::string sFinal;
	sFinal.reserve( nMax );

	size_t n;    
	for (n = 0; n < nMax; ++n) {
		unsigned char z = szSource[n];        
		if (z < 127) {            
			sFinal += (TCHAR)z;        
		}        
		else if (z >= 192 && z <= 223)        
		{
			assert( n < nMax - 1);
			// character is two bytes            
			if (n >= nMax - 1)                 
				break; // something is wrong            
			unsigned char y = szSource[n+1];            
			sFinal += (TCHAR)( (z-192)*64 + (y-128) );            
			n = n + 1;        
		}
		else if (z >= 224 && z <= 239)        
		{            
			// character is three bytes            
			assert( n < nMax - 2);
			if (n >= nMax - 2)                 
				break; // something is wrong            
			unsigned char y = szSource[n+1];            
			unsigned char x = szSource[n+2];            
			sFinal += (TCHAR)( (z-224)*4096 + (y-128)*64 + (x-128) );            
			n = n + 2;        
		}        
		else if (z >= 240 && z <= 247)        
		{            
			// character is four bytes            
			assert( n < nMax - 3);
			if (n >= nMax - 3)                 
				break; // something is wrong            
			unsigned char y = szSource[n+1];            
			unsigned char x = szSource[n+2];            
			unsigned char w = szSource[n+3];            
			sFinal += (TCHAR)( (z-240)*262144 + (y-128)*4096 +
				(x-128)*64 + (w-128) );            
			n = n + 3;        
		}
	else if (z >= 248 && z <= 251)        
		{            
			// character is four bytes            
			assert( n < nMax - 4);
			if (n >= nMax - 4)                 
				break; // something is wrong            
			unsigned char y = szSource[n+1];            
			unsigned char x = szSource[n+2];            
			unsigned char w = szSource[n+3];            
			unsigned char v = szSource[n+4];            
			sFinal += (TCHAR)( (z-248)*16777216 + (y-128)*262144 + 
				(x-128)*4096 + (w-128)*64 + (v-128) );            
			n = n + 4;        
		}        
		else if (z >= 252 && z <= 253)       
		{            
			// character is five bytes            
			assert( n < nMax - 5);
			if (n >= nMax - 5)                 
				break; // something is wrong            
			unsigned char y = szSource[n+1];            
			unsigned char x = szSource[n+2];            
			unsigned char w = szSource[n+3];            
			unsigned char v = szSource[n+4];            
			unsigned char u = szSource[n+5];            
			sFinal += (TCHAR)( (z-252)*1073741824 + (y-128)*16777216 +
				(x-128)*262144 + (w-128)*4096 + (v-128)*64 + (u-128) );
			n = n + 5;        
		}    
	}    
	return sFinal;
}

/*------------------------------------------------------------------
| 
\------------------------------------------------------------------*/
    /*
std::vector<std::string > MKeyValue::getStringVector(const std::string &what) const{
	std::vector<std::string> vData;
	const_iterator it = find (what);
	if (it == end())
		return vData;

	std::string allElems = decodeFromUTF8 ( it->second.c_str(), strlen ( it->second.c_str() ) );
	std::string buf;
	
	stringstream ss(allElems);
	while (ss >> buf) vData.push_back(buf);
	
	return vData;
}

/*------------------------------------------------------------------
| 
\------------------------------------------------------------------
int    MKeyValue::getInt (const char *what, int default_value) const {
	const_iterator it = find (what);

	if (it == end())
		return default_value;
	return atoi ((*it).second.c_str());
}

short MKeyValue::getShort (const char *what, int default_value) const {
	const_iterator it = find (what);

	if (it == end())
		return default_value;
	int value = atoi ((*it).second.c_str());
	if (value < -32768 || value > 35767)
		return default_value;
	return (short)value;
}


template < class T >
void putKey( MKeyValue &k, const char *what, T value ) {
	// Nos libramos del delete?
	std::ostringstream oss;
	oss << value;
	const std::string &buf = oss.str( );
	k[ what ] = buf;
}

void MKeyValue::put( const char *what, int value ) {
	putKey ( *this, what, value);
}
void MKeyValue::put( const char *what, bool value ) {
	putKey ( *this, what, value);
}
void MKeyValue::put( const char *what, float value ) {
	putKey ( *this, what, value);
}

float MKeyValue::getFloat (const char *what, float default_value) const {
	const_iterator it = find (what);

	if (it == end())
		return default_value;
	return (float)atof ((*it).second.c_str());
}

bool MKeyValue::getBool (const char *what, bool default_value) const {
	const_iterator it = find (what);
	if (it == end())
		return default_value;
  // Check agains valid keywords
  const char *yes[] = {"1", "yes", "YES", "Yes", "true", "TRUE", "True"};
  int i = 0;
  for (i=0; i<sizeof (yes)/sizeof(yes[0]); ++i) 
	if ((*it).second == yes[i])
	  return true;
  return false;
}

XMVECTOR MKeyValue::getPoint (const char *what ) const
{
	const_iterator it = find (what);
	if (it == end())
		return XMVectorSet( 0,0,0,0 );
	// it tiene el string "1.2 5.4 3.2"
	const char *values = it->second.c_str();
	float x, y, z;
	int n = sscanf( values, "%f %f %f", &x, &y, &z );
	assert( n == 3 );
	return XMVectorSet( x, y, z, 0.f );
}

CVector2D MKeyValue::getVector2D(const char *what ) const
{
	const_iterator it = find (what);
	if (it == end())
		return CVector2D(0.f, 0.f);
	// it tiene el string "1.2 5.4 3.2"
	const char *values = it->second.c_str();
	float x, z;
	int n = sscanf( values, "%f %f", &x, &z );
	assert( n == 2 );
	return CVector2D(x, z);
}

CPoint2D MKeyValue::getPoint2D(const char *what ) const
{
	const_iterator it = find (what);
	if (it == end())
		return CPoint2D(0.f, 0.f);
	// it tiene el string "1.2 5.4 3.2"
	const char *values = it->second.c_str();
	float x, z;
	int n = sscanf( values, "%f %f", &x, &z );
	assert( n == 2 );
	return CPoint2D(x, z);
}

XMVECTOR MKeyValue::getD3DPoint2D(const char *what) const{
	const_iterator it = find (what);
	if (it == end())
		return XMVectorSet( 0,0,0,1 );
	const char *values = it->second.c_str();
	float x, y;
	int n = sscanf( values, "%f %f", &x, &y);
	assert( n == 2  );
	return XMVectorSet( x, y, 0,1 );
}

XMVECTOR MKeyValue::getQuat (const char *what ) const
{
	const_iterator it = find (what);
	if (it == end())
		return XMVectorSet( 0,0,0,1 );
	const char *values = it->second.c_str();
	float x, y, z, w;
	int n = sscanf( values, "%f %f %f %f", &x, &y, &z, &w );
	assert( n == 4  );
	return XMVectorSet( x, y, z,w );
}

void MKeyValue::getMatrix (const char *what, XMMATRIX &target) const {
	const_iterator it = find (what);
	assert( it != end() || fatal( "Can't find matrix attribute %s\n", what ));
	const char *values = it->second.c_str();
	int n = sscanf( values, 
		"%f %f %f "
		"%f %f %f "
		"%f %f %f "
		"%f %f %f"
	, &target.m[0][0]
	, &target.m[0][1]
	, &target.m[0][2]
	, &target.m[1][0]
	, &target.m[1][1]
	, &target.m[1][2]
	, &target.m[2][0]
	, &target.m[2][1]
	, &target.m[2][2]
	, &target.m[3][0]
	, &target.m[3][1]
	, &target.m[3][2]
	);
	assert( n == 12 || fatal( "Can't read 12 floats from matrix attribute %s. only %d\n", what, n ));
	target.m[0][3] = 0.0f;
	target.m[1][3] = 0.0f;
	target.m[2][3] = 0.0f;
	target.m[3][3] = 1.0f;
}



void MKeyValue::writeSingle (std::ostream &os, const char *what) const {
	os << "<" << what << "\n";
  writeAttributes ( os );
	os << "\t/>\n";
}

void MKeyValue::writeAttributes ( std::ostream &os ) const {
  const_iterator i = begin( );
	while( i != end( ) ) {
		os << "\t" << i->first << "=\"" << i->second << "\"\n";
		++i;
	}
}

void MKeyValue::writeStartElement( std::ostream &os, const char *what) const {
	os << "<" << what << "\n";
  writeAttributes( os );
  os << ">\n";
}

void MKeyValue::writeEndElement( std::ostream &os, const char *what) const {
	os << "</" << what << ">\n";
}*/
